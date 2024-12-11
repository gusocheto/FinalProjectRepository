# E-commerceSite.Web.Application

Welocome to my e-commerce site!

The project is divided into four layers: web; service; data; common.

There are two roles: admin and user. When logged in the new user gets the default user role.
There is one seeded admin which can manage the user(asighn roles, remove roles and delete users). That is done trough the - "Admin controll panel".
Admin can add products, edit products, delete products and manage orders.

Orders can be deleted and their status can be changed. Orders are deleted directly from the database.
Products can be edited, deleted and restocked. The removal of the products is done by softDeleteion.

All of the admin functions can be seen and used olny when logged in with a user that has the specified role.

There are four product types: Male, Female, Kids, Accessories.

Normal users can look at products, see their details and make orders. Products are added to the cart with a many to many relation table that each user has.
From the cart the user can make an order. Fill the order details and make the order. Every user can see their order details after the order is made.
Each order has(location for shipping, the products that are ordered, date on which the order is made and date on which the order is scheduled to arrive,
status of the order and price paid).

The seeded admin email and password are put in the user secrets but for the sake of the projects their is a comment in the program.cs file in web layer that contains
the admin email and password.

