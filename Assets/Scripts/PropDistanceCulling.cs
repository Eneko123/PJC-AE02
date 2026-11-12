using UnityEngine;

public class PropDistanceCulling : MonoBehaviour
{
    [Header("Configuracion")]
    public Transform target;                 // camara o jugador
    public float activateRadius = 25f;
    public float deactivateRadius = 30f;     
    public float checkInterval = 0.25f;

    [Header("Props")]
    public GameObject[] props;
    public Transform sourceRoot;             // padre 

    float _timer;

    void Start()
    {
        if (target == null && Camera.main != null) target = Camera.main.transform;
        Refresh();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < checkInterval) return;
        _timer = 0f;
        Refresh();
    }

    void Refresh()
    {
        if (target == null) return;
        Vector3 p = target.position;
        float sqrAct = activateRadius * activateRadius;
        float sqrDeact = deactivateRadius * deactivateRadius;

        foreach (var go in props)
        {
            if (go == null) continue;
            float sqr = (go.transform.position - p).sqrMagnitude;
            bool active = go.activeSelf;

            if (!active && sqr <= sqrAct) go.SetActive(true);
            else if (active && sqr > sqrDeact) go.SetActive(false);
        }
    }
}