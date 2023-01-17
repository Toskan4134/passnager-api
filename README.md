```postgresql
CREATE TABLE "Profile" (
  "Id" SERIAL PRIMARY KEY,
  "Name" VARCHAR(255) NOT NULL,
  "Icon" BYTEA,
  "Password" VARCHAR(255) NOT NULL,
  "IsActive" BOOLEAN NOT NULL
);

CREATE TABLE "Category" (
  "Id" SERIAL PRIMARY KEY,
  "Name" VARCHAR(255) NOT NULL,
  "Icon" BYTEA,
  "ProfileId" INTEGER NOT NULL REFERENCES "Profile"("Id"),
  "IsActive" BOOLEAN NOT NULL
);

CREATE TABLE "Site" (
  "Id" SERIAL PRIMARY KEY,
  "Site" VARCHAR(255) NOT NULL,
  "Url" VARCHAR(255) NOT NULL,
  "User" VARCHAR(255) NOT NULL,
  "Password" VARCHAR(255) NOT NULL,
  "Description" TEXT,
  "Date" TIMESTAMP WITH TIME ZONE NOT NULL,
  "CategoryId" INTEGER NOT NULL REFERENCES "Category"("Id"),
  "IsActive" BOOLEAN NOT NULL
);
```
docker build --rm -t passnager-api/agaudes:latest .

docker run --rm -p 5106:5106 -p 5107:5107 -e ASPNETCORE_HTTP_PORT=https://+:5107 -e ASPNETCORE_URLS=http://+:5106 -e DB_CONN_STRING="Server=localhost;Database=postgres;Username=postgres;Port=5432" passnager-api/agaudes