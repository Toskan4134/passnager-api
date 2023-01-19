# Passnager Api
## How to Install

### **Option 1 (Docker)**
> 1º Install Docker\
> 2º Execute the command `docker run -p 5106:5106 agaudes/passnager-api:v1`\
> 3º Execute Front-End

### **Option 2 (Local Code)**
> 1º Instal Dotnet SDK 6.0+\
> 2º Run in the current path `dotnet run`\
> 3º Execute Front-End
## Creation Progress
### **Postgresql Tables Created:**

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

### **Docker Commands Used:**
```
docker build -t passnager-api .

docker image tag passnager-api agaudes/passnager-api:v1

docker run -p 5106:5106 agaudes/passnager-api:v1

docker image push agaudes/passnager-api:v1
```