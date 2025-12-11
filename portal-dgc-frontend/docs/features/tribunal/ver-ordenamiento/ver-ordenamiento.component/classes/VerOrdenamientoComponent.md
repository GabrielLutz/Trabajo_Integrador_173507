[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/tribunal/ver-ordenamiento/ver-ordenamiento.component](../README.md) / VerOrdenamientoComponent

# Class: VerOrdenamientoComponent

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L15)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new VerOrdenamientoComponent**(`route`, `router`, `tribunalService`): `VerOrdenamientoComponent`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L29)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### tribunalService

[`TribunalService`](../../../../../core/services/tribunal.service/classes/TribunalService.md)

#### Returns

`VerOrdenamientoComponent`

## Properties

### error

> **error**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L19)

***

### filtroNombre

> **filtroNombre**: `string` = `''`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:26](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L26)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L18)

***

### mensajeExito

> **mensajeExito**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:23](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L23)

***

### ordenamiento

> **ordenamiento**: [`OrdenamientoDetalle`](../../../../../core/models/tribunal.models/interfaces/OrdenamientoDetalle.md) \| `null` = `null`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L16)

***

### ordenamientoId

> **ordenamientoId**: `number`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L17)

***

### posicionesFiltradas

> **posicionesFiltradas**: [`PosicionOrdenamiento`](../../../../../core/models/tribunal.models/interfaces/PosicionOrdenamiento.md)[] = `[]`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:27](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L27)

***

### publicando

> **publicando**: `boolean` = `false`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L22)

## Accessors

### cantidadCuotasAplicadas

#### Get Signature

> **get** **cantidadCuotasAplicadas**(): `number`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:207](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L207)

Total de posiciones que aplican a algún cupo o cuota especial.

##### Returns

`number`

***

### puntajeMaximo

#### Get Signature

> **get** **puntajeMaximo**(): `number`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:180](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L180)

Puntaje más alto encontrado en el ordenamiento.

##### Returns

`number`

***

### puntajePromedio

#### Get Signature

> **get** **puntajePromedio**(): `number`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:191](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L191)

Puntaje promedio de todas las posiciones del ordenamiento.

##### Returns

`number`

## Methods

### cargarOrdenamiento()

> **cargarOrdenamiento**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:48](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L48)

Obtiene del backend el detalle completo del ordenamiento y sus posiciones.

#### Returns

`void`

***

### exportarExcel()

> **exportarExcel**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:132](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L132)

Marcador temporal para exportar el ordenamiento a Excel.

#### Returns

`void`

***

### exportarPDF()

> **exportarPDF**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:125](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L125)

Marcador temporal para exportar el ordenamiento a PDF.

#### Returns

`void`

***

### filtrarPosiciones()

> **filtrarPosiciones**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:73](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L73)

Filtra las posiciones por nombre o cédula para facilitar la búsqueda.

#### Returns

`void`

***

### getEstadoBadgeClass()

> **getEstadoBadgeClass**(`estado`): `string`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:164](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L164)

Clase CSS según el estado de publicación del ordenamiento.

#### Parameters

##### estado

`string`

#### Returns

`string`

***

### getTipoBadgeClass()

> **getTipoBadgeClass**(`tipo`): `string`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:146](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L146)

Clase CSS a aplicar según el tipo de ordenamiento.

#### Parameters

##### tipo

`string`

#### Returns

`string`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:38](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L38)

Recupera el identificador desde la ruta y carga el ordenamiento correspondiente (RF-15).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### publicarOrdenamiento()

> **publicarOrdenamiento**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:92](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L92)

Solicita la publicación definitiva del ordenamiento (RF-15).

#### Returns

`void`

***

### volver()

> **volver**(): `void`

Defined in: [src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts:139](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/ver-ordenamiento/ver-ordenamiento.component.ts#L139)

Regresa al panel principal del tribunal.

#### Returns

`void`
