import React from 'react';
import {
  Box,
  Heading,
  Text,
  Grid,
  GridItem,
  VStack,
  HStack,
  Badge,
  Stat,
  StatLabel,
  StatNumber,
  StatHelpText,
  Divider,
} from '@chakra-ui/react';
import { Client } from '../types/api';

interface ClientInfoProps {
  client: Client;
}

const ClientInfo: React.FC<ClientInfoProps> = ({ client }) => {
  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
    }).format(amount);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  const calculateAge = (birthDate: string) => {
    const today = new Date();
    const birth = new Date(birthDate);
    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();
    
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
      age--;
    }
    
    return age;
  };

  const totalAssets = client.assets?.reduce((sum, asset) => sum + asset.value, 0) || 0;
  const totalLiabilities = client.liabilities?.reduce((sum, liability) => sum + liability.amount, 0) || 0;
  const netWorth = totalAssets - totalLiabilities;
  const totalPensions = client.pensions?.reduce((sum, pension) => sum + pension.currentValue, 0) || 0;

  return (
    <Box bg="white" shadow="lg" borderRadius="lg" p={6}>
      <VStack spacing={6} align="stretch">
        <HStack justify="space-between" align="center">
          <VStack align="start" spacing={1}>
            <Heading size="lg" color="blue.600">
              {client.name}
            </Heading>
            <Text color="gray.600" fontSize="md">
              Client ID: {client.clientId}
            </Text>
          </VStack>
          <VStack align="end" spacing={1}>
            <Badge colorScheme="blue" fontSize="sm" px={3} py={1}>
              Age: {calculateAge(client.dateOfBirth)}
            </Badge>
            <Text fontSize="sm" color="gray.500">
              Born: {formatDate(client.dateOfBirth)}
            </Text>
          </VStack>
        </HStack>

        <Box>
          <Heading size="md" mb={4} color="gray.700">
            Financial Overview
          </Heading>
          <Grid templateColumns="repeat(auto-fit, minmax(200px, 1fr))" gap={4}>
            <GridItem>
              <Stat p={4} bg="blue.50" borderRadius="md">
                <StatLabel>Total Assets</StatLabel>
                <StatNumber color="green.500">{formatCurrency(totalAssets)}</StatNumber>
                <StatHelpText>{client.assets?.length || 0} assets</StatHelpText>
              </Stat>
            </GridItem>
            <GridItem>
              <Stat p={4} bg="red.50" borderRadius="md">
                <StatLabel>Total Liabilities</StatLabel>
                <StatNumber color="red.500">{formatCurrency(totalLiabilities)}</StatNumber>
                <StatHelpText>{client.liabilities?.length || 0} liabilities</StatHelpText>
              </Stat>
            </GridItem>
            <GridItem>
              <Stat p={4} bg="green.50" borderRadius="md">
                <StatLabel>Net Worth</StatLabel>
                <StatNumber color={netWorth >= 0 ? 'green.500' : 'red.500'}>
                  {formatCurrency(netWorth)}
                </StatNumber>
                <StatHelpText>Assets - Liabilities</StatHelpText>
              </Stat>
            </GridItem>
            <GridItem>
              <Stat p={4} bg="purple.50" borderRadius="md">
                <StatLabel>Pension Value</StatLabel>
                <StatNumber color="purple.500">{formatCurrency(totalPensions)}</StatNumber>
                <StatHelpText>{client.pensions?.length || 0} pensions</StatHelpText>
              </Stat>
            </GridItem>
          </Grid>
        </Box>

        {client.financialGoals && client.financialGoals.length > 0 && (
          <>
            <Divider />
            <Box>
              <Heading size="md" mb={4} color="gray.700">
                Financial Goals
              </Heading>
              <VStack spacing={3} align="stretch">
                {client.financialGoals.map((goal) => (
                  <Box
                    key={goal.financialGoalId}
                    p={4}
                    border="1px"
                    borderColor="gray.200"
                    borderRadius="md"
                  >
                    <HStack justify="space-between" align="start">
                      <VStack align="start" spacing={1}>
                        <Text fontWeight="medium">{goal.description}</Text>
                        <Text fontSize="sm" color="gray.500">
                          Target Date: {formatDate(goal.targetDate)}
                        </Text>
                      </VStack>
                      {goal.targetAmount && (
                        <Badge colorScheme="green" fontSize="sm">
                          {formatCurrency(goal.targetAmount)}
                        </Badge>
                      )}
                    </HStack>
                  </Box>
                ))}
              </VStack>
            </Box>
          </>
        )}

        {client.meetingNotes && client.meetingNotes.length > 0 && (
          <>
            <Divider />
            <Box>
              <Heading size="md" mb={4} color="gray.700">
                Latest Meeting Note
              </Heading>
              {client.meetingNotes.slice(0, 1).map((note) => (
                <Box
                  key={note.meetingNoteId}
                  p={4}
                  bg="gray.50"
                  borderRadius="md"
                >
                  <HStack justify="space-between" mb={2}>
                    <Text fontWeight="medium" color="gray.700">
                      {note.author || 'Advisor'}
                    </Text>
                    <Text fontSize="sm" color="gray.500">
                      {formatDate(note.meetingDate)}
                    </Text>
                  </HStack>
                  <Text fontSize="sm" color="gray.600">
                    {note.notes || 'No notes available'}
                  </Text>
                </Box>
              ))}
            </Box>
          </>
        )}
      </VStack>
    </Box>
  );
};

export default ClientInfo;
