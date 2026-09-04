# Flower Forest
## Overview

This is an application where users can manage their portfolio of flowers and share them. They can organise flowers into catalogues e.g. kitchen, bedroom etc. and upload information about them. The application may be updated to allow users to post and comment on other users' portfolios. Users can also make catalogues public or private.

## Technical insights

The repository contains two versions of the backend, the monolithic architecture and the microservices architecture. The microservices architecture uses RabbitMQ and data replication to maintain entity relations across services. The frontend is written in Angular and both versions of the backend are written in dotnet.
