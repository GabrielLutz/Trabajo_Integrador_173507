[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/postulante/components/perfil/perfil.component](../README.md) / PerfilComponent

# Class: PerfilComponent

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L14)

## Implements

- `OnInit`
- `OnDestroy`

## Constructors

### Constructor

> **new PerfilComponent**(`postulanteService`, `router`): `PerfilComponent`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L22)

#### Parameters

##### postulanteService

[`PostulanteService`](../../../../../../core/services/postulante.service/classes/PostulanteService.md)

##### router

`Router`

#### Returns

`PerfilComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L17)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L16)

***

### postulante?

> `optional` **postulante**: [`Postulante`](../../../../../../core/models/postulante.model/interfaces/Postulante.md)

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L15)

## Accessors

### generoDescripcion

#### Get Signature

> **get** **generoDescripcion**(): `string`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:102](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L102)

Devuelve el género para mostrar, contemplando autodefiniciones.

##### Returns

`string`

***

### iniciales

#### Get Signature

> **get** **iniciales**(): `string`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:78](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L78)

Iniciales del postulante para mostrar en el avatar del perfil.

##### Returns

`string`

***

### nombreCompleto

#### Get Signature

> **get** **nombreCompleto**(): `string`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:92](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L92)

Nombre y apellido concatenados listos para la cabecera del perfil.

##### Returns

`string`

## Methods

### cargarPerfil()

> **cargarPerfil**(): `void`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:45](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L45)

Invoca el servicio para obtener el detalle del postulante.

#### Returns

`void`

***

### editarDatos()

> **editarDatos**(): `void`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:71](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L71)

Redirige al formulario de edición de datos personales.

#### Returns

`void`

***

### formatearFecha()

> **formatearFecha**(`fecha`): `string`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:115](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L115)

Formatea la fecha al formato corto utilizado en la UI.

#### Parameters

##### fecha

`string` | `Date`

#### Returns

`string`

***

### mostrarValor()

> **mostrarValor**(`valor?`): `string`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:130](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L130)

Devuelve un guion cuando el valor viene vacío para evitar strings en blanco.

#### Parameters

##### valor?

`string` | `null`

#### Returns

`string`

***

### ngOnDestroy()

> **ngOnDestroy**(): `void`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:37](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L37)

Cancela las suscripciones activas del componente.

#### Returns

`void`

#### Implementation of

`OnDestroy.ngOnDestroy`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/postulante/components/perfil/perfil.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/postulante/components/perfil/perfil.component.ts#L30)

Recupera la información del postulante al ingresar al perfil (RF-01).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`
