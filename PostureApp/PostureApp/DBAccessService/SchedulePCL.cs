using PostureApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostureApp.DBAccessService
{
    public class SchedulePCL
    {
        public static async Task<ScheduleTbl> GetScheduleTbl()
        {
            try
            {
                return await App.scDatabase.GetItemsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
