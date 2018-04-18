Shader "Example/BasicFur" {
	Properties{
		_MainTex("FurColor", 2D) = "white" {}
		_FurHeight("Fur Height",2D) = "white"{}
		_Amount("Height", Range(0,1)) = 0.5
	}
		SubShader{
		Tags{ "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask On
		Cull Off


			CGPROGRAM
#pragma surface surf Lambert vertex:vert

			struct Input {
			float4 color : COLOR;
			float2 uv_MainTex;
		};
		sampler2D _MainTex;
		sampler2D _FurHeight;
		float _Amount;

		void vert(inout appdata_full v) {
			float height = .1f;
			v.vertex.xyz += v.normal * (_Amount*height*.1);
		}
		float _MaxHeight;

		void surf(Input IN, inout SurfaceOutput o) {
			float height = .1f;
			float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
			float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


			float color = hairMapColor.b;


			if (color<height)
				discard;

			//if (hairMapColor.b < 150)
			//	discard;
			o.Albedo = mainColor;
			o.Alpha = 1.1 - height;
		}
		ENDCG

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
			float4 color : COLOR;
			float2 uv_MainTex;
		};
		sampler2D _MainTex;
		sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .2f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .2f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;
		
		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .3f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .3f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG


		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .4f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .4f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .5f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .5f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .6f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .6f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG


		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .7f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .7f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG





		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .8f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .8f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG







		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		struct Input {
		float4 color : COLOR;
		float2 uv_MainTex;
	};
	sampler2D _MainTex;
	sampler2D _FurHeight;
	float _Amount;

	void vert(inout appdata_full v) {
		float height = .9f;
		v.vertex.xyz += v.normal * (_Amount*height*.1);
	}
	float _MaxHeight;

	void surf(Input IN, inout SurfaceOutput o) {
		float height = .9f;
		float4 mainColor = tex2D(_MainTex, IN.uv_MainTex);
		float4 hairMapColor = tex2D(_FurHeight, IN.uv_MainTex);


		float color = hairMapColor.b;


		if (color<height)
			discard;

		//if (hairMapColor.b < 150)
		//	discard;
		o.Albedo = mainColor;
		o.Alpha = 1.1 - height;
	}
	ENDCG














		//this is the base model, this needs to be colored at all times. 
		CGPROGRAM
#pragma surface surf BlinnPhong  vertex:vert
#pragma Standard alpha
		struct Input {
		float2 uv_MainTex;
		float3 worldPos;
	};

	fixed4 _Color;
	float _Amount;
	void vert(inout appdata_full v) {
		//v.vertex.xyz += v.normal*.0001;
	}
	sampler2D _MainTex;
	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

		//clip(frac((IN.worldPos.y + IN.worldPos.z*0.1) * 5) - 0.5);
		o.Albedo = c.rgb ;
	}
	ENDCG
	}
		Fallback "Diffuse"
}