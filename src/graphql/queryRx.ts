import { from, Observable } from 'rxjs';
import { graphQLClient } from './client';

export const query$ = <T>(query: string, variables?: any): Observable<T> => {
    return from(graphQLClient.request<T>(query, variables));
};
