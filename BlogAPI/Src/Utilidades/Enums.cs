using System.Text.Json.Serialization;

namespace BlogAPI.Src.Utilidades
{/// <summary>
 /// <para>Enum para diferenciar nível de acesso</para>
 /// <para>Criado por: Fabio</para>
 /// <para>Versão: 1.0</para>
 /// <para>Data: 18/08/2022</para>
 /// </summary> 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}