
Foreword:
I decided to keep go with an implementation that could be suited for working with DBContext later. 
In the current implementation I'm using the static memory so all data will be destroyed when the application stops.

Thoughts:
The Vehicle interface did not really have much to work with so I decided to add registrationId to the interface.

I decided to have IsTollFreeVehicle and IsTollFreeDate be public to be able to access them from the api.

A major assumption that i did for the CongestionTaxCalculator was that the method GetTax is only supposed to take dates for a single day.
This is filtered in the Controller, all tough one could probably make improvements here to be able to handle multiple days.

For the controller, I did not focus on having data go in through the requestbody but used the querystring fields. 
It would be a nice improvement to be able to send in a JSON with data and populating the dictionary that has the vehicles.

Questions: 

What would be nice to have access points from your perspective?
Since the CongestionTaxCalculator class is replaceable with the current implementation, I do believe that one could extend this to other cities/countries. 

Hopefully my implementation is good enough
Best Regards,
Alex Bergqvist