import React, { useState } from 'react';
import {
  Box,
  Container,
  Heading,
  VStack,
  HStack,
  Input,
  Button,
  Alert,
  AlertIcon,
  Text,
  Spinner,
} from '@chakra-ui/react';
import ClientInfo from './components/ClientInfo';
import WealthSummary from './components/WealthSummary';
import { Client, ClientSummary } from './types/api';
import { apiService } from './services/api';

function App() {
  const [clientId, setClientId] = useState<string>('1');
  const [client, setClient] = useState<Client | null>(null);
  const [summary, setSummary] = useState<ClientSummary | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSearch = async () => {
    if (!clientId || isNaN(Number(clientId))) {
      setError('Please enter a valid client ID');
      return;
    }

    setLoading(true);
    setError(null);
    setClient(null);
    setSummary(null);

    try {
      // Fetch client data
      const clientData = await apiService.getClient(Number(clientId));
      setClient(clientData);

      // Fetch wealth summary
      const summaryResponse = await apiService.getWealthSummary(Number(clientId));
      const parsedSummary = apiService.parseWealthSummary(summaryResponse);
      
      if (parsedSummary) {
        setSummary(parsedSummary);
      } else {
        setError('Failed to parse wealth summary from AI response');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box bg="gray.50" minH="100vh" py={8}>
      <Container maxW="1200px">
        <VStack spacing={8} align="stretch">
          {/* Header */}
          <Box textAlign="center">
            <Heading size="xl" color="blue.600" mb={2}>
              Wealth Summary AI
            </Heading>
            <Text color="gray.600" fontSize="lg">
              AI-powered financial summaries for wealth management
            </Text>
          </Box>

          {/* Search Controls */}
          <Box bg="white" p={6} borderRadius="lg" shadow="md">
            <VStack spacing={4} align="stretch">
              <Heading size="md">Search Client</Heading>
              <HStack>
                <Input
                  placeholder="Enter Client ID"
                  value={clientId}
                  onChange={(e) => setClientId(e.target.value)}
                  type="number"
                  size="lg"
                />
                <Button
                  colorScheme="blue"
                  onClick={handleSearch}
                  isLoading={loading}
                  loadingText="Loading..."
                  size="lg"
                  minW="120px"
                >
                  Search
                </Button>
              </HStack>
            </VStack>
          </Box>

          {/* Error Display */}
          {error && (
            <Alert status="error" borderRadius="lg">
              <AlertIcon />
              {error}
            </Alert>
          )}

          {/* Loading Spinner */}
          {loading && (
            <Box textAlign="center" py={8}>
              <Spinner size="xl" color="blue.500" thickness="4px" />
              <Text mt={4} color="gray.600">
                Generating AI wealth summary...
              </Text>
            </Box>
          )}

          {/* Content */}
          {client && !loading && (
            <VStack spacing={6} align="stretch">
              <ClientInfo client={client} />
              {summary && <WealthSummary summary={summary} />}
            </VStack>
          )}
        </VStack>
      </Container>
    </Box>
  );
}

export default App;
