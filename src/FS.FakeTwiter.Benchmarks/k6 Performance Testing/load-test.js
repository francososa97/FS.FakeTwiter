import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 50000, // 50.000 usuarios virtuales simultáneos
    duration: '60s', // Mantener durante 1 minuto
    thresholds: {
        http_req_duration: ['p(95)<1000'], // 95% bajo 1s
        http_req_failed: ['rate<0.01'], // <1% de fallos
    }
};

export default function () {
    const userId = Math.floor(Math.random() * 1000000);
    const url = `http://localhost:5000/api/test/followers/${userId}`;

    const headers = {
        'X-Correlation-ID': `${__VU}-${__ITER}-${Date.now()}`
    };

    const res = http.get(url, { headers });

    check(res, {
        '✅ status is 200': (r) => r.status === 200,
        '📄 response has body': (r) => r.body && r.body.length > 0,
    });

    sleep(0.1); // Simula un breve delay por usuario
}
