# Giai đoạn 1: Build Node.js
FROM node:20-alpine as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Giai đoạn 2: Chạy bằng Nginx
FROM nginx:alpine
# Vite build ra thư mục dist, nên copy từ dist
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
