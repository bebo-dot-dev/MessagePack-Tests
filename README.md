## S3.Tests
.NET6 AWS S3 client integration tests

### S3 Test Prerequisites
* An active AWS account (free tier is enough)
* A valid access key/secret configured for an IAM user
* A created S3 bucket
* `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` environment variables set to a valid access key/secret

### Nuget packages used
```
PackageReference Include="AWSSDK.S3" Version="3.7.108" />
<PackageReference Include="FluentAssertions" Version="6.11.0" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.3" />
<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
<PackageReference Include="Testcontainers.Minio" Version="3.3.0" />
```

### Build requirements
* .NET6 SDK
* Optional: an IDE i.e. Visual Studio Code / Rider / Visual Studio

### Running the tests
```
$ dotnet test ./S3.Tests/S3.Tests.csproj
  Determining projects to restore...
  All projects are up-to-date for restore.
  S3.Tests -> /home/joe/Code/git-repos/S3.Tests/S3.Tests/bin/Debug/net6.0/S3.Tests.dll
Test run for /home/joe/Code/git-repos/S3.Tests/S3.Tests/bin/Debug/net6.0/S3.Tests.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.3+cc7fb0593127e24f55ce016fb3ac85b5b2857fec
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
EnvironmentVariablesAWSCredentials 1|2023-07-29T14:58:29.772Z|INFO|Credentials found using environment variables.
EnvironmentVariableInternalConfiguration 2|2023-07-29T14:58:29.797Z|INFO|The environment variable AWS_ENABLE_ENDPOINT_DISCOVERY was not set with a value.
EnvironmentVariableInternalConfiguration 3|2023-07-29T14:58:29.797Z|INFO|The environment variable AWS_MAX_ATTEMPTS was not set with a value.
EnvironmentVariableInternalConfiguration 4|2023-07-29T14:58:29.797Z|INFO|The environment variable AWS_RETRY_MODE was not set with a value.
EnvironmentVariableInternalConfiguration 5|2023-07-29T14:58:29.798Z|INFO|The environment variable AWS_EC2_METADATA_SERVICE_ENDPOINT was not set with a value.
EnvironmentVariableInternalConfiguration 6|2023-07-29T14:58:29.798Z|INFO|The environment variable AWS_EC2_METADATA_SERVICE_ENDPOINT_MODE was not set with a value.
EnvironmentVariableInternalConfiguration 7|2023-07-29T14:58:29.798Z|INFO|The environment variable AWS_USE_DUALSTACK_ENDPOINT was not set with a value.
EnvironmentVariableInternalConfiguration 8|2023-07-29T14:58:29.798Z|INFO|The environment variable AWS_USE_FIPS_ENDPOINT was not set with a value.
EnvironmentVariableInternalConfiguration 9|2023-07-29T14:58:29.798Z|INFO|The environment variable AWS_IGNORE_CONFIGURED_ENDPOINT_URLS was not set with a value.
UserCrypto 10|2023-07-29T14:58:29.805Z|INFO|UserCrypto is not supported.  This may be due to use of a non-Windows operating system or Windows Nano Server, or the current user account may not have its profile loaded. Unable to load shared library 'Crypt32.dll' or one of its dependencies. In order to help diagnose loading problems, consider setting the LD_DEBUG environment variable: libCrypt32.dll: cannot open shared object file: No such file or directory
ProfileInternalConfiguration 11|2023-07-29T14:58:29.826Z|INFO|Unable to find a profile named 'default' in store Amazon.Runtime.CredentialManagement.CredentialProfileStoreChain
DefaultConfigurationProvider 12|2023-07-29T14:58:29.837Z|INFO|Resolved DefaultConfigurationMode for RegionEndpoint [eu-west-1] to [Legacy].
EnvironmentVariablesAWSCredentials 13|2023-07-29T14:58:30.004Z|INFO|Credentials found using environment variables.
AWSSDKUtils 14|2023-07-29T14:58:30.050Z|DEBUG|Single encoded /S3.Tests.resources.test-file.txt with endpoint https://test-bucket-unique-unique.s3.eu-west-1.amazonaws.com/ for canonicalization: /S3.Tests.resources.test-file.txt
AmazonS3Client 15|2023-07-29T14:58:30.676Z|DEBUG|Received response (truncated to 1024 bytes): []
AmazonS3Client 16|2023-07-29T14:58:30.695Z|INFO|Request metrics: {"properties":{"AsyncCall":"True","CanonicalRequest":"PUT\n/S3.Tests.resources.test-file.txt\n\ncontent-length:334\ncontent-type:text/plain\nhost:test-bucket-unique-unique.s3.eu-west-1.amazonaws.com\nuser-agent:aws-sdk-dotnet-coreclr/3.7.108.0 aws-sdk-dotnet-core/3.7.108.2 .NET_Core/6.0.20 OS/Linux_5.15.0-76-generic_#83-Ubuntu_SMP_Thu_Jun_15_19:16:32_UTC_2023 ClientAsync\nx-amz-content-sha256:STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER\nx-amz-date:20230729T135830Z\nx-amz-decoded-content-length:4\nx-amz-sdk-checksum-algorithm:SHA256\nx-amz-trailer:x-amz-checksum-sha256\n\ncontent-length;content-type;host;user-agent;x-amz-content-sha256;x-amz-date;x-amz-decoded-content-length;x-amz-sdk-checksum-algorithm;x-amz-trailer\nSTREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER","StringToSign":"AWS4-HMAC-SHA256\n20230729T135830Z\n20230729/eu-west-1/s3/aws4_request\nd03a0a1ef5d85079110970369f4a16ef68a88c9ef6e4019b31da327430caf704","ServiceName":"AmazonS3","ServiceEndpoint":"https://test-bucket-unique-unique.s3.eu-west-1.amazonaws.com/","MethodName":"PutObjectRequest","AmzId2":"x6Xcd4v/8DffrZPORMf0yCX8iEsVX6W+QkAugoFmfzVPrcp0p/eNBHxEjeD7VWafvo3mPzsiUvk=","StatusCode":"OK","BytesProcessed":"0","AWSRequestID":"KS6K89REJB4FBPNJ"},"timings":{"CredentialsRequestTime":1.8105,"RequestSigningTime":62.3623,"HttpRequestTime":566.2856,"ResponseUnmarshallTime":2.211,"ResponseProcessingTime":7.9126,"ClientExecuteTime":818.7543},"counters":{}}
SHA-256 hash: n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 960 ms - /home/joe/Code/git-repos/S3.Tests/S3.Tests/bin/Debug/net6.0/S3.Tests.dll (net6.0)
```

The `resources/test-file.txt` embedded resources file is uploaded to S3

### Output logging
The response side calculated SHA-256 hash is output to the console

`SHA-256 hash: n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=`

AWSS3Client logging is configured to log to the console
```
{
  "properties": {
    "AsyncCall": "True",
    "CanonicalRequest": "PUT\n/S3.Tests.resources.test-file.txt\n\ncontent-length:334\ncontent-type:text/plain\nhost:test-bucket-unique-unique.s3.eu-west-1.amazonaws.com\nuser-agent:aws-sdk-dotnet-coreclr/3.7.108.0 aws-sdk-dotnet-core/3.7.108.2 .NET_Core/6.0.20 OS/Linux_5.15.0-76-generic_#83-Ubuntu_SMP_Thu_Jun_15_19:16:32_UTC_2023 ClientAsync\nx-amz-content-sha256:STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER\nx-amz-date:20230715T202016Z\nx-amz-decoded-content-length:4\nx-amz-sdk-checksum-algorithm:SHA256\nx-amz-trailer:x-amz-checksum-sha256\n\ncontent-length;content-type;host;user-agent;x-amz-content-sha256;x-amz-date;x-amz-decoded-content-length;x-amz-sdk-checksum-algorithm;x-amz-trailer\nSTREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER",
    "StringToSign": "AWS4-HMAC-SHA256\n20230715T202016Z\n20230715/eu-west-1/s3/aws4_request\nb578abeb0f960d0a212884b956d5955ed8b391f19558a995c968a5098b927c35",
    "ServiceName": "AmazonS3",
    "ServiceEndpoint": "https://test-bucket-unique-unique.s3.eu-west-1.amazonaws.com/",
    "MethodName": "PutObjectRequest",
    "AmzId2": "K/tkG3X8rsgUOglFdzNFyrF3UjvVKDiQrOei7ZBA8U11udpuG4B5ixtMz+qeoXcJQiestAh+1Ik=",
    "StatusCode": "OK",
    "BytesProcessed": "0",
    "AWSRequestID": "SRTR24V3E5BZX3T0"
  },
  "timings": {
    "CredentialsRequestTime": 1.5848,
    "RequestSigningTime": 77.7942,
    "HttpRequestTime": 577.0956,
    "ResponseUnmarshallTime": 2.2331,
    "ResponseProcessingTime": 9.5776,
    "ClientExecuteTime": 832.8429
  },
  "counters": {}
}
```

```
PUT
/S3.Tests.resources.test-file.txt

content-length:334
content-type:text/plain
host:test-bucket-unique-unique.s3.eu-west-1.amazonaws.com
user-agent:aws-sdk-dotnet-coreclr/3.7.108.0 aws-sdk-dotnet-core/3.7.108.2 .NET_Core/6.0.20 OS/Linux_5.15.0-76-generic_#83-Ubuntu_SMP_Thu_Jun_15_19:16:32_UTC_2023 ClientAsync
x-amz-content-sha256:STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER
x-amz-date:20230715T202016Z
x-amz-decoded-content-length:4
x-amz-sdk-checksum-algorithm:SHA256
x-amz-trailer:x-amz-checksum-sha256

content-length;content-type;host;user-agent;x-amz-content-sha256;x-amz-date;x-amz-decoded-content-length;x-amz-sdk-checksum-algorithm;x-amz-trailer
STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER
```

### Raw S3 Http Request/Response (http routed through a local proxy)
Request (sensitive parts redacted)
```
PUT http://test-bucket-unique-unique.s3.eu-west-1.amazonaws.com/S3.Tests.resources.test-file.txt HTTP/1.1
Expect: 100-continue
x-amz-sdk-checksum-algorithm: SHA256
User-Agent: aws-sdk-dotnet-coreclr/3.7.108.0 aws-sdk-dotnet-core/3.7.108.2 .NET_Core/6.0.20 OS/Linux_5.15.0-76-generic_#83-Ubuntu_SMP_Thu_Jun_15_19:16:32_UTC_2023 ClientAsync
amz-sdk-invocation-id: 31b1eb96-4a42-438e-ad72-057df319792c
amz-sdk-request: attempt=1; max=5
Host: test-bucket-unique-unique.s3.eu-west-1.amazonaws.com
X-Amz-Date: 20230729T143532Z
X-Amz-Trailer: x-amz-checksum-sha256
X-Amz-Decoded-Content-Length: 4
X-Amz-Content-SHA256: STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER
Authorization: AWS4-HMAC-SHA256 Credential=[redacted]/[redacted]/eu-west-1/s3/aws4_request, SignedHeaders=content-length;content-type;host;user-agent;x-amz-content-sha256;x-amz-date;x-amz-decoded-content-length;x-amz-sdk-checksum-algorithm;x-amz-trailer, Signature=[redacted]
Content-Length: 334
Content-Type: text/plain

4;chunk-signature=4d77cb531cd1d5dcec950de4be7323de208bc32143812753f1f1e3caa818cf58
test
0;chunk-signature=2c36a072d0e02244a6975917d79355f752949d044aa3918cd39aa27eee0110ab
x-amz-checksum-sha256:n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=
x-amz-trailer-signature:9344d95a10fa5b36103b11211d872e776b35ec96c943d88d62b6b08565771215
```
Response:
```
HTTP/1.1 200 OK
x-amz-id-2: b5i26ZOACNbUPJYZfYDLQgcifRu9Noh4nKDPMPuaN35wot9Nn5fibGDJnck6Oju3FTYM7D985R5G272RmiGMvg==
x-amz-request-id: PXEE6AMKV47B35TC
Date: Sat, 29 Jul 2023 14:35:35 GMT
x-amz-server-side-encryption: AES256
ETag: "098f6bcd4621d373cade4e832627b4f6"
x-amz-checksum-sha256: n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=
Server: AmazonS3
Content-Length: 0
```

### Raw MinIO Http Request/Response (http routed through a local proxy)
Request:
```
PUT http://127.0.0.1:49180/test-bucket-unique-unique/S3.Tests.resources.test-file.txt HTTP/1.1
Expect: 100-continue
x-amz-sdk-checksum-algorithm: SHA256
User-Agent: aws-sdk-dotnet-coreclr/3.7.108.0 aws-sdk-dotnet-core/3.7.108.2 .NET_Core/6.0.20 OS/Linux_5.15.0-78-generic_#85-Ubuntu_SMP_Fri_Jul_7_15:25:09_UTC_2023 ClientAsync
amz-sdk-invocation-id: 5330f3f0-819c-4e5e-80d9-48390816467a
amz-sdk-request: attempt=1; max=5
Host: 127.0.0.1:49180
X-Amz-Date: 20230731T181522Z
X-Amz-Trailer: x-amz-checksum-sha256
X-Amz-Decoded-Content-Length: 4
X-Amz-Content-SHA256: STREAMING-AWS4-HMAC-SHA256-PAYLOAD-TRAILER
Authorization: AWS4-HMAC-SHA256 Credential=minio_test/20230731/us-east-1/s3/aws4_request, SignedHeaders=content-length;content-type;host;user-agent;x-amz-content-sha256;x-amz-date;x-amz-decoded-content-length;x-amz-sdk-checksum-algorithm;x-amz-trailer, Signature=1ec8e73bc1901d462da2b8160bc7d12bebb264820563e964bdf9c0fe5baac1df
Content-Length: 334
Content-Type: text/plain

4;chunk-signature=f4f3924c04c4894a340fe53fc8533c661afea801cb271796da93f2cf86257652
test
0;chunk-signature=ba9cfdde0f8e6bd6abd5570eca0612747c561a03f2402846bcd062719011773d
x-amz-checksum-sha256:n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=
x-amz-trailer-signature:6731d8f1dbcb835ac873f1115f494ccdf1e7cacf756cd480b8d16bd8cc782874
```
Response:
```
HTTP/1.1 403 Forbidden
Accept-Ranges: bytes
Content-Length: 513
Content-Type: application/xml
Server: MinIO
Strict-Transport-Security: max-age=31536000; includeSubDomains
Vary: Origin
Vary: Accept-Encoding
X-Amz-Id-2: dd9025bab4ad464b049177c95eb6ebf374d3b3fd1af9251148b658df7ac2e3e8
X-Amz-Request-Id: 1777067E06A63BC7
X-Content-Type-Options: nosniff
X-Xss-Protection: 1; mode=block
Date: Mon, 31 Jul 2023 18:15:23 GMT

<?xml version="1.0" encoding="UTF-8"?>
<Error><Code>SignatureDoesNotMatch</Code><Message>The request signature we calculated does not match the signature you provided. Check your key and signing method.</Message><Key>S3.Tests.resources.test-file.txt</Key><BucketName>test-bucket-unique-unique</BucketName><Resource>/test-bucket-unique-unique/S3.Tests.resources.test-file.txt</Resource><RequestId>1777067E06A63BC7</RequestId><HostId>dd9025bab4ad464b049177c95eb6ebf374d3b3fd1af9251148b658df7ac2e3e8</HostId></Error>
```