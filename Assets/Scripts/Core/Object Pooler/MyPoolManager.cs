using System;
using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviourSingleton<MyPoolManager>
{
    [SerializeField] private PoolSettingSo settings;

    //Diccionario que guarda Listas de objetos por tipo
    private Dictionary<Type, List<IPoolable>> poolablesDictionary = new Dictionary<Type, List<IPoolable>>();

    protected override void OnAwaken()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        if (settings == null)
        {
            Debug.LogWarning("No hay un poolSettingsSo asignado al PoolManager");
            return;
        }

        foreach (PoolSetting setting in settings.poolSettings)
        {
            if (setting.prefab == null)
            {
                Debug.LogWarning("No se contiene Prefab en el So!", gameObject);
                continue;
            }

            IPoolable pooleableInterface = setting.prefab.GetComponent<IPoolable>();

            if (pooleableInterface == null)
            {
                Debug.LogError($"El prefab {setting.prefab.name} no implementa IPooleable");
                continue;
            }

            //Obtenemos el tipo exacto del componente que implementa la interfaz
            Type type = pooleableInterface.GetType();

            //Verificacion que no se haya agregado ya ese tipo
            if (!poolablesDictionary.ContainsKey(type))
            {
                //Add type of dictionary (Bullet, Particles, Enemy, etc.)
                poolablesDictionary.Add(type, new List<IPoolable>());

                //Create conteiner to organize
                GameObject container = new GameObject($"Pool_{type.Name}");

                //Put the container into the PoolManager:
                container.transform.SetParent(this.transform);

                //Create pool for this especific Type
                CreatePool(setting.prefab, container.transform, setting.quantity, poolablesDictionary[type]);
            }

            Debug.Log($"Pool inicializado: {string.Join(", ", poolablesDictionary.Keys)}");
        }
    }

    private void CreatePool(GameObject prefab, Transform parent, int quantity, List<IPoolable> list)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(prefab, parent);        //1.Instanciamos el Prefab
            IPoolable poolable = go.GetComponent<IPoolable>();  //2.Obtenemos el componente IPooleable
            poolable.Deactivate();                              //3.Desactivamos el objeto para que no este activo al inicio 
            list.Add(poolable);                                 //4.Agregamos el objeto a su lista
        }
    }

    // MonoBehaviour porque es un GameObject
    // Le agregamos la restricci n IPooleable para mayor seguridad
    public T GetInstanceFromPool<T> () where T : MonoBehaviour, IPoolable 
    {
        Type type = typeof(T);

        //Obtenemos la lista de objetos del tipo solicitado. Si no existe, mostramos un error.
        if(poolablesDictionary.TryGetValue(type, out List<IPoolable> poolList))
        {
            for (int i = 0; i < poolList.Count; i++)
                if (!poolList[i].IsActive)
                    return poolList[i] as T;

            //Si llegamos aqui, nos quedamos sin objetos en la lista
            Debug.LogWarning($"Se agotaron los objetos de tipo {type.Name} en la pool.");
            return null;
        }

        //Si llegamos aqui, el tipo solicitado no existe en la pool.
        Debug.LogWarning($"El tipo {type.Name} no existe en la pool. Falta agregarlo al So.");
        return null;
    }
}