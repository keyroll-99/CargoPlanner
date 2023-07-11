FROM osrm/osrm-backend  as builder

WORKDIR /map


COPY ./maps/* /map

ARG OSM_FILE=poland-latest.osm.pbf
ARG OSRM_FILE=poland-latest.osm

ENV OSM_FILE=$OSM_FILE
ENV OSRM_FILE=$OSRM_FILE

RUN osrm-extract -p /opt/car.lua /map/$OSM_FILE

RUN rm -rf $OSM_FILE

RUN osrm-partition /map/$OSRM_FILE
RUN osrm-customize /map/$OSRM_FILE



CMD  osrm-routed --algorithm mld  /map/$OSRM_FILE

EXPOSE 5000