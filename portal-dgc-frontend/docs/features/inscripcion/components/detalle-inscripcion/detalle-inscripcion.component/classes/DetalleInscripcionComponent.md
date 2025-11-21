[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component](../README.md) / DetalleInscripcionComponent

# Class: DetalleInscripcionComponent

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L14)

## Implements

- `OnInit`
- `OnDestroy`

## Constructors

### Constructor

> **new DetalleInscripcionComponent**(`route`, `router`, `inscripcionService`): `DetalleInscripcionComponent`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:21](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L21)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### inscripcionService

[`InscripcionService`](../../../../../../core/services/inscripcion.service/classes/InscripcionService.md)

#### Returns

`DetalleInscripcionComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L17)

***

### inscripcion

> **inscripcion**: [`InscripcionResponse`](../../../../../../core/models/inscripcion.model/interfaces/InscripcionResponse.md) \| `null` = `null`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L15)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L16)

## Methods

### getEstadoBadgeClass()

> **getEstadoBadgeClass**(`estado`): `string`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:86](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L86)

Devuelve la clase de badge para el estado de la inscripción.

#### Parameters

##### estado

`string`

#### Returns

`string`

***

### irAListado()

> **irAListado**(): `void`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:79](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L79)

Vuelve al listado de inscripciones del postulante.

#### Returns

`void`

***

### ngOnDestroy()

> **ngOnDestroy**(): `void`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:71](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L71)

Cancela las suscripciones reactivas activas.

#### Returns

`void`

#### Implementation of

`OnDestroy.ngOnDestroy`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L30)

Recupera la inscripción seleccionada según el parámetro de ruta (RF-05).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### tieneAutodefinicion()

> **tieneAutodefinicion**(): `boolean`

Defined in: [src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts:111](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/detalle-inscripcion/detalle-inscripcion.component.ts#L111)

Indica si la inscripción posee datos de autodefinición cargados.

#### Returns

`boolean`
