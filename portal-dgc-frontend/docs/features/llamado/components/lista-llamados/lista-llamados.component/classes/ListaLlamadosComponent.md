[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/llamado/components/lista-llamados/lista-llamados.component](../README.md) / ListaLlamadosComponent

# Class: ListaLlamadosComponent

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L14)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new ListaLlamadosComponent**(`llamadoService`, `router`): `ListaLlamadosComponent`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:21](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L21)

#### Parameters

##### llamadoService

[`LlamadoService`](../../../../../../core/services/llamado.service/classes/LlamadoService.md)

##### router

`Router`

#### Returns

`ListaLlamadosComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L18)

***

### estadoSeleccionado

> **estadoSeleccionado**: `"activos"` \| `"inactivos"` = `'activos'`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L19)

***

### llamadosActivos

> **llamadosActivos**: [`Llamado`](../../../../../../core/models/llamado.model/interfaces/Llamado.md)[] = `[]`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L15)

***

### llamadosInactivos

> **llamadosInactivos**: [`Llamado`](../../../../../../core/models/llamado.model/interfaces/Llamado.md)[] = `[]`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L16)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L17)

## Accessors

### llamadosVisibles

#### Get Signature

> **get** **llamadosVisibles**(): [`Llamado`](../../../../../../core/models/llamado.model/interfaces/Llamado.md)[]

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:95](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L95)

Conjunto de llamados visibles según el filtro actual.

##### Returns

[`Llamado`](../../../../../../core/models/llamado.model/interfaces/Llamado.md)[]

## Methods

### cargarLlamados()

> **cargarLlamados**(): `void`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:36](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L36)

Consulta el backend para obtener los llamados segmentados por estado.

#### Returns

`void`

***

### getDiasRestantes()

> **getDiasRestantes**(`fechaCierre`): `number`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:78](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L78)

Calcula los días restantes hasta la fecha de cierre para el badge informativo.

#### Parameters

##### fechaCierre

`string` | `Date`

#### Returns

`number`

***

### inscribirse()

> **inscribirse**(`llamadoId`): `void`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:69](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L69)

Inicia el flujo de inscripción usando el llamado elegido.

#### Parameters

##### llamadoId

`number`

#### Returns

`void`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L29)

Carga llamados activos e inactivos al iniciar la pantalla (RF-03).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### seleccionarEstado()

> **seleccionarEstado**(`estado`): `void`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:88](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L88)

Permite alternar entre la vista de llamados activos e inactivos.

#### Parameters

##### estado

`"activos"` | `"inactivos"`

#### Returns

`void`

***

### verDetalle()

> **verDetalle**(`llamadoId`): `void`

Defined in: [src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts:62](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/lista-llamados/lista-llamados.component.ts#L62)

Navega al detalle del llamado seleccionado.

#### Parameters

##### llamadoId

`number`

#### Returns

`void`
