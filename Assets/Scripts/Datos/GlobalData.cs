﻿using System.Collections;
using System.Collections.Generic;

public static class GlobalData{

	#region VARIABLES ESTADOS
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
	#endregion

	#region VARIABLES_CANTIDAD_ESTADOS
	public const int ANCHO_TABLERO = 3;
	public const int ALTO_TABLERO = 3;
	public const int VALORES_SALUD = 3;
	public const int VALORES_CARGAS = 3;
	public const int VALORES_POWER_UP = 2;
	public const int VALORES_DISTANCA_ENEMIGO = 2;
	public const int VALORES_SALUD_ENEMIGO = 4;
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
	public const int ESCUDO = 5;
	public const int CARGAR_DISPARO = 6;
	public const int BAZOONGA = 7;
	#endregion

}