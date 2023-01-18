Postgresql Tables Created:

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

Docker Commands Used:
```
docker build --rm -t passnager-api/agaudes:latest .

docker run --rm passnager-api/agaudes
```