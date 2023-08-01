using AutoMapper;
using BasicIC.Common;
using BasicIC.Models.Common;
using BasicIC.Models.Main;
using Common;
using Common.Commons;
using Common.Interfaces;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Repositories;
using BasicIC.Common;


using BasicIC.Models.Common;

using BasicIC.Models.Main;
using Settings.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using BasicIC.Interfaces;


namespace BasicIC.Services.Implement
{
    // Map T (View Model) to V (Entity Model)
    public class BaseCRUDService<T, V> : BaseService, IBaseCRUDService<T, V> where T : BaseModel where V : class
    {
        protected BaseRepositorySql<V> _repo;
        
        public BaseCRUDService(BaseRepositorySql<V> repo,
            IConfigManager config, ILogger logger, IMapper mapper) : base(config, logger, mapper)
        {
            _repo = repo;
        }

        public virtual async Task<ResponseService<T>> Create(T obj, DbContext dbContext = null, bool autoLog = true)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                obj.AddInfo();
                V vData;
                try
                {
                    vData = _mapper.Map<T, V>(obj);
                }
                catch (Exception ex)
                {
                    return new ResponseService<T>(ex.Message).BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }
                V result = await _repo.Create(vData, dbContext);

                // Push kafka log message
                //if (autoLog)
                //{
                //    string createJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //    await this.CreateKafkaLog(obj.id, Constants.LOG_USER_CREATE, createJson);
                //    // create log action
                //    await _logActionService.CreateKafkaLog(LogType.CREATE, typeof(T).Name.Replace("Model", ""),
                //                                            $"Create {typeof(T).Name.Replace("Model", "")}", obj, "");
                //}

                return new ResponseService<T>(_mapper.Map<V, T>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<T>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public virtual async Task<ResponseService<ListResult<T>>> GetAll(PagingParam param, DbContext dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Set tenant_id
                String tenant_id = SessionStore.Get(Constants.KEY_SESSION_TENANT_ID);
                if (!String.IsNullOrEmpty(tenant_id))
                    param.tenant_id = tenant_id;

                // Get result from Entity
                ListResult<V> resultEntity = await _repo.GetAll(param, dbContext);

                // Map List of Entity items to View items
                List<T> items;
                try
                {
                    items = _mapper.Map<List<V>, List<T>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<T>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<T> result = new ListResult<T>(items, resultEntity.total);

                return new ResponseService<ListResult<T>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<T>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public virtual async Task<ResponseService<ListResult<T>>> GetAllNoTenantId(PagingParam param, DbContext dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Set tenant_id
                param.tenant_id = null;

                // Get result from Entity
                ListResult<V> resultEntity = await _repo.GetAll(param, dbContext);

                // Map List of Entity items to View items
                List<T> items;
                try
                {
                    items = _mapper.Map<List<V>, List<T>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<T>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<T> result = new ListResult<T>(items, resultEntity.total);

                return new ResponseService<ListResult<T>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<T>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public virtual async Task<ResponseService<T>> GetById(ItemModel obj, DbContext dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get result from Entity
                V result = await _repo.GetById(new Guid(obj.id), dbContext);

                // Map result to View
                T resultView;
                try
                {
                    resultView = _mapper.Map<V, T>(result);
                }
                catch
                {
                    return new ResponseService<T>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                return new ResponseService<T>(resultView);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<T>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public virtual async Task<ResponseService<bool>> Delete(ItemModel obj, DbContext dbContext = null, bool autoLog = true)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var entity = await _repo.GetById(new Guid(obj.id));
                V result = await _repo.Delete(new Guid(obj.id), dbContext);

                if (result != null)
                {
                    return new ResponseService<bool>(true);
                }
                else
                    return new ResponseService<bool>(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        public virtual async Task<(ResponseService<T>, T)> Update(T obj, DbContext dbContext = null, bool autoLog = true)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                // Get V from db
                V resultDb = await _repo.GetById(obj.id, dbContext);

                if (resultDb == null)
                {
                    return (new ResponseService<T>("Record not found").BadRequest(ErrorCodes.RECORD_NOT_FOUND), null);
                }

                // Map V to T then update common fields
                T TResultDb;
                try
                {
                    TResultDb = _mapper.Map<V, T>(resultDb);
                }
                catch
                {
                    return (new ResponseService<T>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS), null);
                }
                obj.UpdateInfo(TResultDb);

                // Map new updated T to V then update V on db
                V vData;
                try
                {
                    vData = _mapper.Map(obj, resultDb);
                }
                catch
                {
                    return (new ResponseService<T>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS), TResultDb);
                }
                V result = await _repo.Update(vData, dbContext);



                // Push kafka log message
                //if (autoLog)
                //{
                //    string originalData = Newtonsoft.Json.JsonConvert.SerializeObject(TResultDb);
                //    string updateData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //    await this.CreateKafkaLog(obj.id, Constants.LOG_USER_UPDATE, originalData, updateData);
                //    // create log action
                //    await _logActionService.CreateKafkaLog(LogType.UPDATE, typeof(T).Name.Replace("Model", ""),
                //                                            $"Update {typeof(T).Name.Replace("Model", "")}", TResultDb, vData);
                //}

                return (new ResponseService<T>(_mapper.Map<V, T>(result)), TResultDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return (new ResponseService<T>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR), null);
            }
        }

        public virtual async Task<ResponseService<ListResult<T>>> GetAllGlobalSearch(PagingParamGlobalSearch param, DbContext dbContext = null)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                ListResult<V> resultEntity = await _repo.GetAllGlobalSearch(param, dbContext);

                // Map List of Entity items to View items
                List<T> items;
                try
                {
                    items = _mapper.Map<List<V>, List<T>>(resultEntity.items);
                }
                catch
                {
                    return new ResponseService<ListResult<T>>("Error mapping models").BadRequest(ErrorCodes.ERROR_MAPPING_MODELS);
                }

                ListResult<T> result = new ListResult<T>(items, resultEntity.total);

                return new ResponseService<ListResult<T>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<T>>(ex.Message).BadRequest(ErrorCodes.UNHANDLED_ERROR);
            }
        }

        #region helper function
        // Send kafka message for changes
        //protected async Task CreateKafkaLog(Guid idReference, string logType, string originalDataJson, string updateData = null)
        //{
        //    // Don't log if using secret key
        //    bool isSecretKey = SessionStore.Get(Constants.KEY_SESSION_IS_SECRET_KEY);
        //    if (isSecretKey)
        //        return;

        //    // Create new model
        //    KafkaUserLogModel kafkaUserLog = new KafkaUserLogModel();
        //    kafkaUserLog.AddInfo();
        //    kafkaUserLog.message = "";

        //    // Set updateData to empty if null
        //    if (updateData == null)
        //        updateData = "";

        //    // Map data to model
        //    kafkaUserLog.status = true;
        //    kafkaUserLog.log_type = logType;
        //    kafkaUserLog.object_name = typeof(T).Name;
        //    kafkaUserLog.service = "Settings";
        //    kafkaUserLog.reference_id = idReference;
        //    kafkaUserLog.original_data = originalDataJson;
        //    kafkaUserLog.update_data = updateData;

        //    // create producer
        //    ProducerWrapper<object> _producer = new ProducerWrapper<object>();
        //    await _producer.CreateMess(Topic.LOG_CRUD_SETTINGS, kafkaUserLog);
        //}

        // True when duplicate is found
        protected async Task<bool> CheckDuplicate(T obj, string[] fields, DbContext dbContext)
        {
            try
            {
                PagingParam param = new PagingParam();

                // Add search field duplicate to search param
                if (fields.Length > 0)
                {
                    foreach (string field in fields)
                    {
                        SearchParam searchField = new SearchParam();
                        searchField.name_field = field;
                        searchField.value_search = obj.GetType().GetProperty(field).GetValue(obj);

                        param.field_get_list.Add(searchField);
                    }
                }
                else
                {
                    throw new InternalServiceException("Fields to check duplicate is empty", ErrorCodes.DUPLICATE_FIELD_EMPTY);
                }

                // Get of duplicate from Entity
                ListResult<V> entityList = await _repo.GetAll(param, dbContext);

                if (entityList.total == 0)
                    return false;

                // Map to T
                List<T> modelList = _mapper.Map<List<V>, List<T>>(entityList.items);

                // Get duplicate without dupping id
                List<T> duplicateList = modelList.Where(m => m.id != obj.id).ToList();

                if (duplicateList.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (InternalServiceException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new InternalServiceException("Error checking duplicate", ErrorCodes.ERROR_CHECKING_DUPLICATE);
            }

        }
        #endregion
    }
}