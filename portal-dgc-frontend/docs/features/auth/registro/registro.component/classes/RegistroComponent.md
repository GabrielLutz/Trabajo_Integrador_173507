[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/auth/registro/registro.component](../README.md) / RegistroComponent

# Class: RegistroComponent

Defined in: [src/app/features/auth/registro/registro.component.ts:12](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L12)

## Constructors

### Constructor

> **new RegistroComponent**(): `RegistroComponent`

#### Returns

`RegistroComponent`

## Properties

### form

> **form**: `FormGroup`\<`ɵNonNullableFormControls`\<\{ `apellido`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `cedulaIdentidad`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `celular`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `contrasena`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `email`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `nombre`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `usuario`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; \}\>\>

Defined in: [src/app/features/auth/registro/registro.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L19)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/auth/registro/registro.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L17)

## Accessors

### f

#### Get Signature

> **get** **f**(): `ɵNonNullableFormControls`\<\{ `apellido`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `cedulaIdentidad`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `celular`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `contrasena`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `email`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `nombre`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `usuario`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; \}\>

Defined in: [src/app/features/auth/registro/registro.component.ts:32](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L32)

Controles expuestos para simplificar el template.

##### Returns

`ɵNonNullableFormControls`\<\{ `apellido`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `cedulaIdentidad`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `celular`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `contrasena`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `email`: (`string` \| (`control`) => `ValidationErrors` \| `null`[])[]; `nombre`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; `usuario`: (`string` \| (`control`) => `ValidationErrors` \| `null`)[]; \}\>

## Methods

### onCedulaInput()

> **onCedulaInput**(`event`): `void`

Defined in: [src/app/features/auth/registro/registro.component.ts:39](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L39)

Formatea la cédula de identidad en tiempo real respetando el patrón uruguayo.

#### Parameters

##### event

`Event`

#### Returns

`void`

***

### onSubmit()

> **onSubmit**(): `void`

Defined in: [src/app/features/auth/registro/registro.component.ts:81](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/registro/registro.component.ts#L81)

Realiza el registro simulado y redirige al login con mensaje de confirmación (RF-02).

#### Returns

`void`
