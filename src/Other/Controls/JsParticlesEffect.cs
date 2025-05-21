using CSHTML5.Native.Html.Controls;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSilver.Samples.Showcase;

public class JsParticlesEffect : HtmlPresenter
{
    private object _domElement;

    public JsParticlesEffect()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Set the initial content:
        Html = "<div></div>";

        // Get a JS reference to that initial content:
        _domElement = Interop.GetDiv(this);

        // Load the JS library:
        await LoadJSLibrary();

        await Task.Delay(100);

        // Start the effect:
        Interop.ExecuteJavaScriptVoidAsync($@"
            //$0.firstChild.style.width = ""100%"";
            //$0.firstChild.style.height = ""100%"";
            $0.firstChild.firstChild.style.width = ""100%"";
            $0.firstChild.firstChild.style.height = ""100%"";
            $0.style.overflow = ""hidden"";
          window.ParticleEffect?.startEffect($0.firstChild.firstChild);
        ", _domElement);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        // Stop the effect and release all resources:
        Interop.ExecuteJavaScriptVoidAsync($@"
          window.ParticleEffect?.stopEffect();
        ", _domElement);
    }

    private static async Task LoadJSLibrary()
    {
        if (await FileLoader.TryLoadJavaScriptFile("https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"))
        {
            // Load our script for the particles effect:
            Interop.ExecuteJavaScriptVoid($@"
                window.ParticleEffect = {{
                    scene: null,
                    camera: null,
                    renderer: null,
                    particleSystem: null,
                    animationFrameId: null,
                    mouseX: 0,
                    mouseY: 0,
                    targetRotationX: 0,
                    targetRotationY: 0,
                    isMouseMoving: false,
                    lastMouseMoveTime: 0,
                    velocityX: 0,
                    velocityY: 0,
                    friction: 0.95,
                    onMouseMove: null,

                    // Debounce helper
                    debounce(fn, wait) {{
                        let timeout;
                        return function(...args) {{
                            clearTimeout(timeout);
                            timeout = setTimeout(() => fn.apply(this, args), wait);
                        }};
                    }},

                    // Start the particle effect in the specified div
                    startEffect(div) {{
                        if (!(div instanceof HTMLElement)) {{
                            console.error('ParticleEffect.startEffect: Parameter must be a DOM element');
                            return;
                        }}

                        // Stop any existing effect to prevent conflicts
                        if (this.renderer || this.animationFrameId) {{
                            this.stopEffect();
                        }}

                        // Scene setup
                        this.scene = new THREE.Scene();
                        this.camera = new THREE.PerspectiveCamera(75, div.clientWidth / div.clientHeight, 0.1, 1000);
                        this.camera.position.z = 50;

                        // Create the renderer
                        this.renderer = new THREE.WebGLRenderer({{ antialias: true, alpha: true }});
                        this.renderer.setClearColor(0x000000, 0);
                        const canvas = this.renderer.domElement;

                        // Add a smooth CSS transition on the canvas
                        canvas.style.transition = ""width 150ms ease-in-out, height 150ms ease-in-out"";

                        // Set its initial size via both pixel buffer and style
                        this.renderer.setSize(div.clientWidth, div.clientHeight, false);
                        canvas.style.width  = div.clientWidth  + ""px"";
                        canvas.style.height = div.clientHeight + ""px"";

                        div.appendChild(canvas);

                        // Create circular sprite texture
                        const createSpriteTexture = () => {{
                            const canvas = document.createElement('canvas');
                            canvas.width = 64;
                            canvas.height = 64;
                            const ctx = canvas.getContext('2d');
                            const gradient = ctx.createRadialGradient(32, 32, 0, 32, 32, 32);
                            gradient.addColorStop(0, 'rgba(255, 255, 255, 0.9)');
                            gradient.addColorStop(0.7, 'rgba(255, 255, 255, 0.4)');
                            gradient.addColorStop(1, 'rgba(255, 255, 255, 0)');
                            ctx.fillStyle = gradient;
                            ctx.fillRect(0, 0, 64, 64);
                            return new THREE.CanvasTexture(canvas);
                        }};

                        // Particle system
                        const particleCount = 400;
                        const particles = new THREE.BufferGeometry();
                        const positions = new Float32Array(particleCount * 3);
                        const colors = new Float32Array(particleCount * 3);

                        const colorPalette = [
                            new THREE.Color(0xff6666), // Soft red
                            new THREE.Color(0x66cc66), // Soft green
                            new THREE.Color(0x6666ff), // Soft blue
                            new THREE.Color(0xffcc66), // Soft yellow
                            new THREE.Color(0xcc66cc)  // Soft purple
                        ];

                        for (let i = 0; i < particleCount * 3; i += 3) {{
                            positions[i] = (Math.random() - 0.5) * 100;
                            positions[i + 1] = (Math.random() - 0.5) * 100;
                            positions[i + 2] = (Math.random() - 0.5) * 100;
                            const color = colorPalette[Math.floor(Math.random() * colorPalette.length)];
                            colors[i] = color.r;
                            colors[i + 1] = color.g;
                            colors[i + 2] = color.b;
                        }}

                        particles.setAttribute('position', new THREE.BufferAttribute(positions, 3));
                        particles.setAttribute('color', new THREE.BufferAttribute(colors, 3));

                        const particleMaterial = new THREE.PointsMaterial({{
                            size: 3.5,
                            map: createSpriteTexture(),
                            vertexColors: true,
                            transparent: true,
                            opacity: 0.85,
                            blending: THREE.NormalBlending,
                            depthWrite: false
                        }});

                        this.particleSystem = new THREE.Points(particles, particleMaterial);
                        this.scene.add(this.particleSystem);

                        // Reset state variables
                        this.mouseX = 0;
                        this.mouseY = 0;
                        this.targetRotationX = 0;
                        this.targetRotationY = 0;
                        this.isMouseMoving = false;
                        this.lastMouseMoveTime = 0;
                        this.velocityX = 0;
                        this.velocityY = 0;

                        // Mouse move handler
                        this.onMouseMove = (event) => {{
                            const newMouseX = (event.clientX / window.innerWidth) * 2 - 1;
                            const newMouseY = -(event.clientY / window.innerHeight) * 2 + 1;
                            this.velocityX = (newMouseX - this.mouseX) * 0.1;
                            this.velocityY = (newMouseY - this.mouseY) * 0.1;
                            this.mouseX = newMouseX;
                            this.mouseY = newMouseY;
                            this.targetRotationY = this.mouseX * Math.PI * 0.1;
                            this.targetRotationX = this.mouseY * Math.PI * 0.1;
                            this.isMouseMoving = true;
                            this.lastMouseMoveTime = Date.now();
                        }};
                        document.addEventListener('mousemove', this.onMouseMove);

                        // Create a debounced resize handler
                        const debouncedResize = this.debounce((width, height) => {{
                            //this.camera.aspect = width / height;
                            //this.camera.updateProjectionMatrix();
                            //this.renderer.setSize(width, height);

                            // update the camera
                            this.camera.aspect = width / height;
                            this.camera.updateProjectionMatrix();

                            // resize the internal buffer, but do not clobber style
                            this.renderer.setSize(width, height, false);

                            // now gently transition the *display* size
                            canvas.style.width  = width  + ""px"";
                            canvas.style.height = height + ""px"";
                        }}, 150);

                        // Window resize handler
                        this.onResize = () => {{
                            debouncedResize(window.innerWidth, window.innerHeight);
                        }};
                        window.addEventListener('resize', this.onResize);

                        // Initialize with current window size
                        debouncedResize(window.innerWidth, window.innerHeight);

                        // Animation loop
                        const animate = () => {{
                            this.animationFrameId = requestAnimationFrame(animate);

                            if (Date.now() - this.lastMouseMoveTime > 500) {{
                                this.isMouseMoving = false;
                            }}

                            // Update particle positions
                            const positions = this.particleSystem.geometry.attributes.position.array;
                            for (let i = 0; i < positions.length; i += 3) {{
                                positions[i] += Math.sin(Date.now() * 0.001 + positions[i + 2]) * 0.02;
                                positions[i + 1] += Math.cos(Date.now() * 0.001 + positions[i]) * 0.02;
                                positions[i + 2] += Math.sin(Date.now() * 0.001 + positions[i + 1]) * 0.02;
                                if (Math.abs(positions[i]) > 50) positions[i] *= -0.9;
                                if (Math.abs(positions[i + 1]) > 50) positions[i + 1] *= -0.9;
                                if (Math.abs(positions[i + 2]) > 50) positions[i + 2] *= -0.9;
                            }}
                            this.particleSystem.geometry.attributes.position.needsUpdate = true;

                            // Apply rotation with inertia
                            this.particleSystem.rotation.x += (this.targetRotationX - this.particleSystem.rotation.x) * 0.05;
                            this.particleSystem.rotation.y += (this.targetRotationY - this.particleSystem.rotation.y) * 0.05;

                            if (!this.isMouseMoving) {{
                                this.particleSystem.rotation.x += this.velocityX;
                                this.particleSystem.rotation.y += this.velocityY;
                                this.velocityX *= this.friction;
                                this.velocityY *= this.friction;
                                if (Math.abs(this.velocityX) < 0.001 && Math.abs(this.velocityY) < 0.001) {{
                                    this.particleSystem.rotation.y += 0.001;
                                }}
                            }}

                            this.renderer.render(this.scene, this.camera);
                        }};

                        animate();
                    }},

                    // Stop the effect and release resources
                    stopEffect() {{
                        // Stop animation loop
                        if (this.animationFrameId) {{
                            cancelAnimationFrame(this.animationFrameId);
                            this.animationFrameId = null;
                        }}

                        // Remove event listeners
                        if (this.onMouseMove) {{
                            document.removeEventListener('mousemove', this.onMouseMove);
                            this.onMouseMove = null;
                        }}
                        if (this.onResize) {{
                            window.removeEventListener('resize', this.onResize);
                            this.onResize = null;
                        }}

                        // Dispose of Three.js resources
                        if (this.particleSystem) {{
                            this.scene.remove(this.particleSystem);
                            this.particleSystem.geometry.dispose();
                            if (this.particleSystem.material.map) {{
                                this.particleSystem.material.map.dispose();
                            }}
                            this.particleSystem.material.dispose();
                            this.particleSystem = null;
                        }}

                        if (this.renderer) {{
                            if (this.renderer.domElement && this.renderer.domElement.parentNode) {{
                                this.renderer.domElement.parentNode.removeChild(this.renderer.domElement);
                            }}
                            this.renderer.dispose();
                            this.renderer.forceContextLoss();
                            this.renderer = null;
                        }}

                        this.scene = null;
                        this.camera = null;
                        this.mouseX = 0;
                        this.mouseY = 0;
                        this.targetRotationX = 0;
                        this.targetRotationY = 0;
                        this.velocityX = 0;
                        this.velocityY = 0;

                        console.log('ParticleEffect stopped and resources released');
                    }}
                }};
                ");
        }
    }
}
