docker build -f Dockerfile.test.build -t com-danliris-service-spinning-webapi:test-build . 
docker create --name com-danliris-service-spinning-webapi-test-build-container com-danliris-service-spinning-webapi:test-build 
mkdir -p ./bin/publish
docker cp com-danliris-service-spinning-webapi-test-build-container:/out/. ./bin/publish
docker build -f Dockerfile.test -t com-danliris-service-spinning-webapi:test