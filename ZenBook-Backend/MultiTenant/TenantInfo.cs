using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;

namespace ZenBook_Backend.MultiTenant
{
    public class TenantInfo : ITenantInfo
    {
        
        public string Id { get; set; }            
        public string Identifier { get; set; }    
        public string Name { get; set; }           
        
        public string ConnectionString { get; set; }
    }
}
