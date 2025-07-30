import { GraphQLClient } from 'graphql-request';

const endpoint = 'https://localhost:7070/graphql';

export const graphQLClient = new GraphQLClient(endpoint);
