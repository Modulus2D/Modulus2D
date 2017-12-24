FROM mono:latest

RUN mono --version
RUN mv Example/Resources/* ExampleServer/bin/x64/Debug
RUN mono ExampleServer/bin/x64/Debug/ExampleServer.exe

EXPOSE 14357