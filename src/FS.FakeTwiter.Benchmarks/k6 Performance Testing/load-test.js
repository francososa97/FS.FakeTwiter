import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '10s', target: 100 },       // 0 → 100 usuarios
        { duration: '30s', target: 1000 },      // 100 → 1000
        { duration: '1m', target: 100000 },     // 1K → 100K
        { duration: '2m', target: 1000000 },    // ⚠️ 100K → 1M
        { duration: '30s', target: 0 }          // bajada
    ],
    thresholds: {
        http_req_duration: ['p(95)<1000'],   // el 95% debe estar bajo 1s
        http_req_failed: ['rate<0.01'],      // menos del 1% fallos
    }
};

export default function () {
    const userId = Math.floor(Math.random() * 1000000);
    const res = http.get(`http://localhost:5000/api/test/followers/${userId}`);

    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    sleep(0.1);
}