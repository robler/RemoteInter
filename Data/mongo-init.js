let error = true;

// Document: https://docs.mongodb.com/manual/reference/method/ (mongosh)

db = new Mongo().getDB("main");

db.createCollection("ticket", { capped: false });


//Ticket default
var ticketJson = cat('/var/mongo_ticket.json');
var ticketArray = JSON.parse(ticketJson);
db.ticket.insertMany(ticketArray);

