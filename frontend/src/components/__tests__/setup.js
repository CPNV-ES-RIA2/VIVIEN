import { afterAll, afterEach, beforeAll } from 'vitest'
import { HttpResponse, http } from 'msw'
import { setupServer } from 'msw/node'

const labels = [
    {name: 'test', confidence: 0.1},
    {name: 'test', confidence: 0.1},
    {name: 'test', confidence: 0.1},
    {name: 'test', confidence: 0.1}
]

export const restHandlers = [
    http.post('http://localhost/analyze', () => {
        return HttpResponse.json(labels)
    }),
]

const server = setupServer(...restHandlers)

beforeAll(() => server.listen({ onunhandledRequest: 'error' }))

afterAll(() => server.close())

afterEach(() => server.resetHandlers())