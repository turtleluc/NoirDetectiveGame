shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
uniform float projector_power : hint_range(0,1) = 0.5;
uniform sampler2D distortionTexture;
uniform float vignette_param: hint_range(1,10)=20.0;

float noise(vec2 input) {
    return fract(sin(dot(input, vec2(3.1415, 8952.37)) * 12.29) * 93.116);
}

void fragment() {
    // Vignette-related:
    float vignette_param2 = vignette_param + 0.5 * noise(vec2(TIME / 60.0, TIME / 59.0));
    float vig = -vignette_param2 * ((UV.x - 0.5) * (UV.x - 0.5) + (UV.y - 0.5) * (UV.y - 0.5));
    vec4 vignette = vec4(vig, vig, vig, 1.0);

    // Grayscale-related:
    vec4 pixelcolor = texture(SCREEN_TEXTURE, SCREEN_UV);
    float brightness = (0.299 * pixelcolor.r + 0.487 * pixelcolor.g + 0.114 * pixelcolor.b);
    vec4 grayscale = vec4(brightness, brightness, brightness, pixelcolor.a);

    // Random fluctuation:
    vec4 random = texture(distortionTexture, UV);

    // Combining the parts:
    vec4 grayscale2 = mix(random, vignette, 0.7);
    COLOR = mix(grayscale, grayscale2, projector_power);
}
