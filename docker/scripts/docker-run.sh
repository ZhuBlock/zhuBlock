#!/bin/bash

set -e

if [ $# -eq 0 ]; then
    echo "Command is missing, try again."
    exit 1
fi

USERID=`id -u`
GROUPID=`id -g`

docker-compose run --rm -u${USERID}:${GROUPID} zhu-block ${@:1}
