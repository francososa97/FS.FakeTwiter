import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 1000000,
    duration: '60s',
    insecureSkipTLSVerify: true, // evita problemas con certificado local
    thresholds: {
        http_req_duration: ['p(95)<1000'], // el 95% debajo de 1s
        http_req_failed: ['rate<0.01'],    // menos del 1% fallos
    },
};

export default function () {
    const userId = crypto.randomUUID(); // genera un UUID por request
    const url = `https://localhost:7069/api/Follow/followers/${userId}`;

    const headers = {
        'accept': 'application/json',
        'X-API-KEY': 'super-secret-key',
        'X-Correlation-ID': `${__VU}-${__ITER}-${Date.now()}`
    };

    const res = http.get(url, { headers });

    check(res, {
        '✅ status is 200': (r) => r.status === 200,
        '📄 response has body': (r) => r.body && r.body.length > 0,
    });

    sleep(0.1);
}
