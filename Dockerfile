FROM ubuntu:latest
LABEL authors="sherry"

ENTRYPOINT ["top", "-b"]