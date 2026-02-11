import { GraphQLClient } from 'graphql-request';

export const graphQLClient = new GraphQLClient("http://localhost:5169/graphql", {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
});
