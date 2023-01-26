namespace ClientManager.Client.Services.CostumerService
{
    public interface ICostumerService
    {
        List<Costumer> Costumers { get; set; }
        Task GetCostumers();
        Task<Costumer> GetSingleCostumer(int id);

        Task CreateCostumer (Costumer costumer);
        Task UpdateCostumer (Costumer costumer);
        Task DeleteCostumer (int id);
    }
}
