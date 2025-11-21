[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/postulante/components/mis-inscripciones/mis-inscripciones.component](../README.md) / MisInscripcionesComponent

# Class: MisInscripcionesComponent

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:12](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L12)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new MisInscripcionesComponent**(`inscripcionService`, `router`): `MisInscripcionesComponent`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L18)

#### Parameters

##### inscripcionService

[`InscripcionService`](../../../../../../core/services/inscripcion.service/classes/InscripcionService.md)

##### router

`Router`

#### Returns

`MisInscripcionesComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L15)

***

### inscripciones

> **inscripciones**: [`InscripcionSimple`](../../../../../../core/models/inscripcion.model/interfaces/InscripcionSimple.md)[] = `[]`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L13)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L14)

## Methods

### cargarInscripciones()

> **cargarInscripciones**(): `void`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:33](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L33)

Recupera desde el servicio todas las inscripciones del postulante logueado.

#### Returns

`void`

***

### getEstadoBadgeClass()

> **getEstadoBadgeClass**(`estado`): `string`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:66](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L66)

Determina la clase CSS del badge según el estado de la inscripción.

#### Parameters

##### estado

`string`

#### Returns

`string`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:26](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L26)

Carga las inscripciones del postulante al inicializar la vista (RF-05).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### trackByInscripcion()

> **trackByInscripcion**(`_`, `inscripcion`): `number`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:91](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L91)

Permite optimizar *ngFor devolviendo la clave primaria de la inscripción.

#### Parameters

##### \_

`number`

##### inscripcion

[`InscripcionSimple`](../../../../../../core/models/inscripcion.model/interfaces/InscripcionSimple.md)

#### Returns

`number`

***

### verDetalle()

> **verDetalle**(`inscripcionId`): `void`

Defined in: [src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts:59](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/mis-inscripciones/mis-inscripciones.component.ts#L59)

Navega al detalle de inscripción seleccionado.

#### Parameters

##### inscripcionId

`number`

#### Returns

`void`
