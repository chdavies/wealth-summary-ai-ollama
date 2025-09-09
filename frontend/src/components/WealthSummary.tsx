import React from 'react';
import {
  Box,
  Heading,
  Text,
  VStack,
  HStack,
  Grid,
  GridItem,
  Badge,
  Divider,
} from '@chakra-ui/react';
import { ClientSummary } from '../types/api';

interface WealthSummaryProps {
  summary: ClientSummary;
}

const WealthSummary: React.FC<WealthSummaryProps> = ({ summary }) => {
  const { client_summary } = summary;

  return (
    <Box bg="white" shadow="lg" borderRadius="lg" p={6}>
      <VStack spacing={8} align="stretch">
        <HStack align="center" spacing={3}>
          <VStack align="start" spacing={1}>
            <Heading size="lg" color="green.600">
              AI-Generated Wealth Summary
            </Heading>
            <Text color="gray.600" fontSize="md">
              Professional financial review powered by AI
            </Text>
          </VStack>
          <Badge colorScheme="green" variant="subtle" ml="auto" px={3} py={1}>
            AI Analysis
          </Badge>
        </HStack>

        <Box>
          <Heading size="md" color="gray.700" mb={4}>
            Executive Summary
          </Heading>
          <Box
            p={5}
            bg="gray.50"
            borderRadius="md"
            border="1px"
            borderColor="gray.200"
          >
            <Text fontSize="md" lineHeight={1.7} color="gray.700">
              {client_summary.overall_summary}
            </Text>
          </Box>
        </Box>

        <Divider />

        <Box>
          <Heading size="md" color="gray.700" mb={4}>
            Financial Position
          </Heading>
          <Grid templateColumns="repeat(auto-fit, minmax(300px, 1fr))" gap={4}>
            <GridItem>
              <Box p={4} border="1px" borderColor="gray.200" borderRadius="md">
                <Text fontWeight="semibold" color="green.600" mb={2}>
                  Assets
                </Text>
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.financial_position.assets}
                </Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} border="1px" borderColor="gray.200" borderRadius="md">
                <Text fontWeight="semibold" color="red.600" mb={2}>
                  Liabilities
                </Text>
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.financial_position.liabilities}
                </Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} border="1px" borderColor="gray.200" borderRadius="md">
                <Text fontWeight="semibold" color="blue.600" mb={2}>
                  Income & Expenditure
                </Text>
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.financial_position.income_expenditure}
                </Text>
              </Box>
            </GridItem>
            <GridItem>
              <Box p={4} border="1px" borderColor="gray.200" borderRadius="md">
                <Text fontWeight="semibold" color="purple.600" mb={2}>
                  Pensions
                </Text>
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.financial_position.pensions}
                </Text>
              </Box>
            </GridItem>
          </Grid>
        </Box>

        <Divider />

        <Grid templateColumns="repeat(auto-fit, minmax(400px, 1fr))" gap={6}>
          <GridItem>
            <Box>
              <Heading size="md" color="gray.700" mb={4}>
                Progress Since Last Meeting
              </Heading>
              <Box
                p={4}
                bg="orange.50"
                borderRadius="md"
                border="1px"
                borderColor="orange.200"
              >
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.progress_since_last_meeting}
                </Text>
              </Box>
            </Box>
          </GridItem>

          <GridItem>
            <Box>
              <Heading size="md" color="gray.700" mb={4}>
                Financial Goals
              </Heading>
              <Box
                p={4}
                bg="purple.50"
                borderRadius="md"
                border="1px"
                borderColor="purple.200"
              >
                <Text fontSize="sm" color="gray.700" lineHeight={1.6}>
                  {client_summary.financial_goals}
                </Text>
              </Box>
            </Box>
          </GridItem>
        </Grid>

        <Divider />

        <Box>
          <Heading size="md" color="gray.700" mb={4}>
            Recommendations & Next Steps
          </Heading>
          <Box
            p={5}
            bg="teal.50"
            borderRadius="md"
            border="1px"
            borderColor="teal.200"
          >
            <Text fontSize="md" color="gray.700" lineHeight={1.7}>
              {client_summary.recommendations_and_next_steps}
            </Text>
          </Box>
        </Box>

        <Box mt={6} pt={4} borderTop="1px" borderColor="gray.200">
          <HStack spacing={2}>
            <Badge colorScheme="yellow" variant="subtle">
              AI Generated
            </Badge>
            <Text fontSize="xs" color="gray.500">
              This summary was generated by AI and should be reviewed by a qualified financial advisor.
              Please verify all recommendations before taking action.
            </Text>
          </HStack>
        </Box>
      </VStack>
    </Box>
  );
};

export default WealthSummary;
