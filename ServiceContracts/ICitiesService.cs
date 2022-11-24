namespace ServiceContracts
{
    //This is a contract means a requirement that the developer of controller has made to the developer of service. Both the Controller dev and Service dev should respect this contract and inherit from this interface to provide funtionality to the controller. This is called Dependency Inversion Principle

    //The advantage here is that Controller dev can use this interface to build its Controller as it is now depending on this Contract, earlier it was directly creating an object of Service and using it. Service may or may not have been build at the time of Controller development. So, this is a good practice to use Contracts instead of directly creating objects of Service. This is called Dependency Inversion Principle
    public interface ICitiesService
    {
        Guid ServiceInstanceId { get; }
        List<string> GetCities();
    }
}