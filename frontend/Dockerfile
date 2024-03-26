FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

COPY ./substitue-envrionment-variables.sh /docker-entrypoint.d/40-substitue-envrionment-variables.sh
RUN chmod +x /docker-entrypoint.d/40-substitue-envrionment-variables.sh