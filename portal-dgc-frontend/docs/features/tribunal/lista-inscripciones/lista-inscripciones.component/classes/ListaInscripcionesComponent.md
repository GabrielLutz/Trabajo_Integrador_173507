[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/tribunal/lista-inscripciones/lista-inscripciones.component](../README.md) / ListaInscripcionesComponent

# Class: ListaInscripcionesComponent

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L15)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new ListaInscripcionesComponent**(`route`, `router`, `tribunalService`): `ListaInscripcionesComponent`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L30)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### tribunalService

[`TribunalService`](../../../../../core/services/tribunal.service/classes/TribunalService.md)

#### Returns

`ListaInscripcionesComponent`

## Properties

### departamentos

> **departamentos**: `string`[] = `[]`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:27](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L27)

***

### error

> **error**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L19)

***

### estados

> **estados**: `string`[] = `[]`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L28)

***

### filtroDepartamento

> **filtroDepartamento**: `string` = `''`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:23](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L23)

***

### filtroEstado

> **filtroEstado**: `string` = `''`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:24](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L24)

***

### filtroNombre

> **filtroNombre**: `string` = `''`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L22)

***

### filtroUniverso

> **filtroUniverso**: `string` = `''`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:25](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L25)

***

### inscripciones

> **inscripciones**: [`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)[] = `[]`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L16)

***

### inscripcionesFiltradas

> **inscripcionesFiltradas**: [`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)[] = `[]`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L17)

***

### llamadoId

> **llamadoId**: `number`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:20](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L20)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L18)

## Methods

### aplicarFiltros()

> **aplicarFiltros**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:83](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L83)

Aplica los filtros seleccionados sobre la lista original.

#### Returns

`void`

***

### cargarInscripciones()

> **cargarInscripciones**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:49](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L49)

Consulta al backend las inscripciones pendientes de evaluación.

#### Returns

`void`

***

### extraerFiltros()

> **extraerFiltros**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:75](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L75)

Construye los valores disponibles para los filtros desplegables.

#### Returns

`void`

***

### getEstadoBadgeClass()

> **getEstadoBadgeClass**(`estado`): `string`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:134](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L134)

Devuelve la clase del badge según el estado calculado.

#### Parameters

##### estado

`string`

#### Returns

`string`

***

### getEstadoEvaluacion()

> **getEstadoEvaluacion**(`inscripcion`): `string`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:118](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L118)

Determina el estado de avance de evaluación de la inscripción.

#### Parameters

##### inscripcion

[`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)

#### Returns

`string`

***

### getProgresoEvaluacion()

> **getProgresoEvaluacion**(`inscripcion`): `number`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:150](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L150)

Calcula el porcentaje de evaluación completada para mostrar en la UI.

#### Parameters

##### inscripcion

[`InscripcionParaEvaluar`](../../../../../core/models/tribunal.models/interfaces/InscripcionParaEvaluar.md)

#### Returns

`number`

***

### limpiarFiltros()

> **limpiarFiltros**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:107](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L107)

Restablece los filtros y muestra todas las inscripciones.

#### Returns

`void`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:39](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L39)

Carga las inscripciones del llamado seleccionado al iniciar la vista (RF-11).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### verDetalle()

> **verDetalle**(`inscripcionId`): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:159](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L159)

Abre el detalle de evaluación de la inscripción seleccionada.

#### Parameters

##### inscripcionId

`number`

#### Returns

`void`

***

### volver()

> **volver**(): `void`

Defined in: [src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts:166](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/lista-inscripciones/lista-inscripciones.component.ts#L166)

Retorna al dashboard principal del tribunal.

#### Returns

`void`
