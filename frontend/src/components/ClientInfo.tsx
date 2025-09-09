import React from 'react';
import {
  Box,
  Heading,
  Text,
  Grid,
  GridItem,
  Stack,
  Flex,
  Badge,
} from '@chakra-ui/react';
import { Client } from '../types/api';

interface ClientInfoProps {
  client: Client;
}

const ClientInfo: React.FC<ClientInfoProps> = ({ client }) => {
  const cardBg = 'white';
  const borderColor = 'gray.200';

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
    <Box bg={cardBg} shadow="lg" borderRadius="lg" p={6}>
      <Stack gap={6}>
        <Flex justify="space-between" align="center">
          <Stack gap={1}>
            <Heading size="lg" color="blue.600">
              {client.name}
            </Heading>
            <Text color="gray.600" fontSize="md">
              Client ID: {client.clientId}
            </Text>
          </Stack>
          <Stack gap={1} align="end">
            <Badge colorScheme="blue" fontSize="sm" px={3} py={1}>
              Age: {calculateAge(client.dateOfBirth)}
            </Badge>
            <Text fontSize="sm" color="gray.500">
              Born: {formatDate(client.dateOfBirth)}
            </Text>
          </Stack>
        </Flex>

        <Box>
          <Heading size="md" mb={4} color="gray.700">
            Financial Overview
          </Heading>
          <Grid templateColumns="repeat(auto-fit, minmax(200px, 1fr))" gap={4}>
            <GridItem>
              <Box p={4} bg="blue.50" borderRadius="md" textAlign="center">
                <Text fontSize="sm" color="gray.600" mb={1}>Total Assets</Text>
                <Text fontSize="2xl" fontWeight="bold" color="green.500">{formatCurrency(totalAssets)}</Text>
                <Text fontSize="xs" color="gray.500">{client.assets?.length || 0} assets</Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} bg="red.50" borderRadius="md" textAlign="center">
                <Text fontSize="sm" color="gray.600" mb={1}>Total Liabilities</Text>
                <Text fontSize="2xl" fontWeight="bold" color="red.500">{formatCurrency(totalLiabilities)}</Text>
                <Text fontSize="xs" color="gray.500">{client.liabilities?.length || 0} liabilities</Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} bg="green.50" borderRadius="md" textAlign="center">
                <Text fontSize="sm" color="gray.600" mb={1}>Net Worth</Text>
                <Text fontSize="2xl" fontWeight="bold" color={netWorth >= 0 ? 'green.500' : 'red.500'}>
                  {formatCurrency(netWorth)}
                </Text>
                <Text fontSize="xs" color="gray.500">Assets - Liabilities</Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} bg="purple.50" borderRadius="md" textAlign="center">
                <Text fontSize="sm" color="gray.600" mb={1}>Pension Value</Text>
                <Text fontSize="2xl" fontWeight="bold" color="purple.500">{formatCurrency(totalPensions)}</Text>
                <Text fontSize="xs" color="gray.500">{client.pensions?.length || 0} pensions</Text>
              </Box>
            </GridItem>
          </Grid>
        </Box>

        {client.financialGoals && client.financialGoals.length > 0 && (
          <Box>
            <Heading size="md" mb={4} color="gray.700">
              Financial Goals
            </Heading>
            <Stack gap={3}>
              {client.financialGoals.map((goal) => (
                <Box
                  key={goal.financialGoalId}
                  p={4}
                  border="1px"
                  borderColor={borderColor}
                  borderRadius="md"
                >
                  <Flex justify="space-between" align="start">
                    <Stack gap={1}>
                      <Text fontWeight="medium">{goal.description}</Text>
                      <Text fontSize="sm" color="gray.500">
                        Target Date: {formatDate(goal.targetDate)}
                      </Text>
                    </Stack>
                    {goal.targetAmount && (
                      <Badge colorScheme="green" fontSize="sm">
                        {formatCurrency(goal.targetAmount)}
                      </Badge>
                    )}
                  </Flex>
                </Box>
              ))}
            </Stack>
          </Box>
        )}

        {client.meetingNotes && client.meetingNotes.length > 0 && (
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
                <Flex justify="space-between" mb={2}>
                  <Text fontWeight="medium" color="gray.700">
                    {note.author || 'Advisor'}
                  </Text>
                  <Text fontSize="sm" color="gray.500">
                    {formatDate(note.meetingDate)}
                  </Text>
                </Flex>
                <Text fontSize="sm" color="gray.600">
                  {note.notes || 'No notes available'}
                </Text>
              </Box>
            ))}
          </Box>
        )}
      </Stack>
    </Box>
  );
};

export default ClientInfo;
