using System.Collections;
using System.Collections.Generic;

public static class GlobalData{

	/*#region VARIABLES ESTADOS
	public const int SALUD_BAJA = 0;
	public const int SALUD_MEDIA = 1;
	public const int SALUD_ALTA = 2;
	public const int SIN_CARGAS = 0;
	public const int UNA_CARGA = 1;
	public const int VARIAS_CARGAS = 2;
	public const int HAY_POWER_UP = 0;
	public const int NO_HAY_POWER_UP = 1;
	public const int ENEMIGO_A_TIRO = 0;
	public const int ENEMIGO_FUERA_ALCANCE = 1;
	public const int ENEMIGO_MUERTO = 0;
	public const int ENEMIGO_POCA_SALUD = 1;
	public const int ENEMIGO_MEDIA_SALUD = 2;
	public const int ENEMIGO_TODA_SALUD = 3;
	#endregion*/

	#region VARIABLES_CANTIDAD_ESTADOS
	public const int ALTO_TABLERO = 3;
	public const int ANCHO_TABLERO = 3;
	public const int VALORES_SALUD = 4;
	public const int VALORES_CARGAS = 6; //para ir de 0 a 5
	public const int VALORES_ESCUDO = 2;
	public const int VALORES_DISTANCA_ENEMIGO = 2;
	public const int VALORES_SALUD_ENEMIGO = 4;
	public const int VALORES_ESCUDO_ENEMIGO = 2;
    public const int VALORES_CARGA_ENEMIGO = 6;  //para ir de 0 a 5
	public const int TOTAL_ESTADOS = ANCHO_TABLERO * ALTO_TABLERO * VALORES_SALUD * VALORES_CARGAS * VALORES_ESCUDO * VALORES_SALUD_ENEMIGO * VALORES_CARGA_ENEMIGO * VALORES_DISTANCA_ENEMIGO * VALORES_ESCUDO_ENEMIGO;
	#endregion

	#region INDICES ARRAY ESTADO
	public const int FILA = 0;
	public const int COLUMNA = 1;
	public const int SALUD = 2;
	public const int CARGAS = 3;
	public const int ESCUDOS = 4;
	public const int ENEMIGO_EN_RANGO = 5;
	public const int SALUD_ENEMIGO = 6;
	public const int ESCUDO_ENEMIGO = 7;
	public const int CARGAS_ENEMIGO = 8;
	#endregion

	#region VARIABLES PARAMETROS JUEGO
	public const int MAX_TURNOS = 30;
	public const int TOTAL_ACCIONES = 8;
	#endregion

	#region ACCIONES
	public const int MOVER_ARRIBA = 0;
	public const int MOVER_ABAJO = 1;
	public const int MOVER_IZQ = 2;
	public const int MOVER_DER = 3;
	public const int DISPARO = 4;
	public const int ESCUDO_ACCION = 5;
	public const int CARGAR_DISPARO = 6;
	public const int BAZOONGA = 7;
	#endregion

}