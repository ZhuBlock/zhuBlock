FROM node:8.11.1
MAINTAINER Dorde Kesic <dordekesic.keso@gmail.com>

ARG user=root

RUN npm config set registry http://registry.npmjs.org && \
    npm install -g yarn

USER $user
