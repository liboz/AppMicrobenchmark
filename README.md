# Instructions on how to run locally

Run one of the following commands

```
docker build -f ./DockerfileGo . -t benchmark
docker build -f ./DockerfileDotNet . -t benchmark
```

Then run:

```
docker run -p 8080:8080 benchmark
```

Test Command:

```
ab -p benchmark.txt -T 'application/json' -n 5000 -c 100 http://localhost:8080/benchmark
```
