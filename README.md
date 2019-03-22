# crext-Web-Api
Documentation For USSD GATE-WAY Api 

Base Api Url http://crextapicore20190322102801.azurewebsites.net/api/

Get Info Organisation:  /Organisation/{id}
eg http://crextapicore20190322102801.azurewebsites.net/api//Organisation/26

returns 
{"id":26,"name":"chiken ","rcNumber":null,"natureOfBusiness":null,"phoneNumber":null,"businessAddress":null,....}


2. Get Customer Info : /Customers/GetCustomerByPhone/{phoneNumber}
eg http://crextapicore20190322102801.azurewebsites.net/api/Customers/GetCustomerByPhone/8063368817
returns 
{"id":"21826eb2-5f46-4499-bb9b-08d6a6996f60","firstName":null,....}

3. Get Operator TeamMemberId : TeamMember/GetTeamMemberIdByPhone/{phoneNumber}
eg http://crextapicore20190322102801.azurewebsites.net/api/TeamMember/GetTeamMemberIdByPhone/908948934
returns 



4 Update Customer Review : POST   review/Create
{
 "review": {
 customerId : "94af237e-98b8-4b2c-6b0f-08d685fb1a5e",
 teamMemberId: 1,
 comment: "USer Updated Successfully "
 },
 
 "reviewNotification": {
 reviewKindId: 1,
 ReviewActionId: 1,
 }
 
}
reviewKindId and ReviewActionId values 

if AddPayment was Selected  then Set
ReviewActionId: 3 
reviewKindId: 1,
else If Report was Selected then set

ReviewActionId: 5 
reviewKindId: 1,
