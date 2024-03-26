import { afterAll, afterEach, beforeAll } from 'vitest';
import { HttpResponse, http } from 'msw';
import { setupServer } from 'msw/node';

import { config } from '@vue/test-utils';
import i18n from '../../i18n/index.js';

config.global.plugins = [i18n];

const labels = [
    {name: 'cucumber', confidence: 0.1},
    {name: 'potatoes', confidence: 0.1},
    {name: 'carrots', confidence: 0.1},
    {name: 'salad', confidence: 0.1}
]

export const restHandlers = [
    http.post('VITE_API_GATEWAY_URL_PLACEHOLDER/Analyze', () => {
        return HttpResponse.json(labels)
    }),
]

const server = setupServer(...restHandlers)

beforeAll(() => server.listen({ onunhandledRequest: 'error' }))

afterAll(() => server.close())

afterEach(() => server.resetHandlers())