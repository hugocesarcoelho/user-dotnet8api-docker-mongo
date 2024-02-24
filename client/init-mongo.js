db.createUser(
    {
        user: "admin",
        pwd: "root",
        roles: [
            {
                role: "readWrite",
                db: "UserDB"
            }
        ]
    }
);
db.createCollection("User");

db.User.insert({
    "_id": "90700C24-1459-41AD-A16C-1A2756C7ADB0",
    "Name": "Admin",
    "Username": "admin",
    "Email": "admin@test.org",
    "PhoneNumber": "+15555222551"
});