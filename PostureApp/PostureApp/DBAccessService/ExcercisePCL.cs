using PostureApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostureApp.DBAccessService
{
    public class ExcercisePCL
    {
        public static async Task<List<MovementListTbl>> GetMovementListTbl()
        {
            try
            {
                return await App.mvDatabase.GetItemsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
