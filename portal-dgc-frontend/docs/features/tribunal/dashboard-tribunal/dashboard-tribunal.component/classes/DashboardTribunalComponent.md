[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/tribunal/dashboard-tribunal/dashboard-tribunal.component](../README.md) / DashboardTribunalComponent

# Class: DashboardTribunalComponent

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:12](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L12)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new DashboardTribunalComponent**(`tribunalService`, `router`): `DashboardTribunalComponent`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:20](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L20)

#### Parameters

##### tribunalService

[`TribunalService`](../../../../../core/services/tribunal.service/classes/TribunalService.md)

##### router

`Router`

#### Returns

`DashboardTribunalComponent`

## Properties

### error

> **error**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L15)

***

### estadisticas

> **estadisticas**: [`EstadisticasTribunal`](../../../../../core/models/tribunal.models/interfaces/EstadisticasTribunal.md) \| `null` = `null`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L13)

***

### llamadoId

> **llamadoId**: `number` = `1`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L18)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L14)

## Methods

### calcularPorcentaje()

> **calcularPorcentaje**(`valor`, `total`): `number`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:73](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L73)

Calcula el porcentaje para las tarjetas del dashboard.

#### Parameters

##### valor

`number`

##### total

`number`

#### Returns

`number`

***

### cargarEstadisticas()

> **cargarEstadisticas**(): `void`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:35](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L35)

Consulta al backend los indicadores clave para el tribunal.

#### Returns

`void`

***

### irAGenerarOrdenamiento()

> **irAGenerarOrdenamiento**(): `void`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:66](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L66)

Navega al flujo de generación de ordenamiento final (RF-14).

#### Returns

`void`

***

### irAInscripciones()

> **irAInscripciones**(): `void`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:59](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L59)

Navega a la lista de inscripciones pendientes de evaluación (RF-11).

#### Returns

`void`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L28)

Carga las estadísticas del llamado asignado al iniciar el panel (RF-11).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### obtenerTotalGeneral()

> **obtenerTotalGeneral**(): `number`

Defined in: [src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts:80](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/dashboard-tribunal/dashboard-tribunal.component.ts#L80)

Devuelve la cantidad de inscripciones que no aplican a cupos especiales.

#### Returns

`number`
