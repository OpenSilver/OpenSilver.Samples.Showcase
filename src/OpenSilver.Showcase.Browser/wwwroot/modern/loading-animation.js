(function () {
    let lastAnimationTime = 0;
    const ANIMATION_THROTTLE_DELAY = 150; // Minimum time between animations
    const ANIMATION_DURATION = 150; // Total animation duration

    const odometerClass = "opensilver-odometer";

    function startLoader() {
        const count = document.querySelectorAll("." + odometerClass);
        const loader = document.querySelector(".opensilver-loader-progress-bar");
        const loaderProgress = document.querySelector(".opensilver-loader-progress");
        if (!count || !loader) return;

        loader.style.width = "0%";
        const observer = new MutationObserver(updateCount);

        function updateCount() {
            const loadPercentageText = getComputedStyle(document.documentElement)
                .getPropertyValue("--blazor-load-percentage-text")
                .trim();
            const loadPercentage = parseInt(loadPercentageText.replace(/"/g, ""));
            const currentValue = isNaN(loadPercentage) ? 0 : loadPercentage;

            // Always animate 100 regardless of throttling
            if (currentValue === 100) {
                animateCounter(currentValue, true);
                loader.style.width = "100%";
                loaderProgress.style["border-right"] = "none";
                observer.disconnect();
                return;
            }

            // Throttle animations to prevent too frequent updates
            const now = Date.now();
            if (now - lastAnimationTime >= ANIMATION_THROTTLE_DELAY) {
                animateCounter(currentValue);
                lastAnimationTime = now;
            }

            loader.style.width = currentValue + "%";
        }

        observer.observe(document.documentElement, {
            attributes: true,
            attributeFilter: ["style"],
        });
        updateCount();
    }

    function animateCounter(newValue, force = false) {
        const odometers = Array.from(document.querySelectorAll("." + odometerClass));
        const newValueString = String(newValue).padStart(3, "0");

        for (let index = odometers.length - 1; index > -1; index--) {
            const element = odometers[index];

            if (force) {
                const finalOdometer = document.createElement("div");
                finalOdometer.textContent = newValueString[index];
                finalOdometer.classList.add(odometerClass);
                element.replaceWith(finalOdometer);
                continue;
            }

            if (element.textContent === "") {
                element.textContent = "0";
            }
            const currentValue = element.textContent || "0";

            if (newValueString[index] !== currentValue) {
                element.style.transition = `transform ${ANIMATION_DURATION}ms, opacity ${ANIMATION_DURATION}ms`;
                element.style.transform = "translateY(-4px)";
                element.style.opacity = "0.5";

                setTimeout(() => {
                    element.textContent = newValueString[index];
                    element.style.transform = "translateY(4px)";
                    element.style.opacity = "0.5";

                    setTimeout(() => {
                        element.style.transform = "translateY(0)";
                        element.style.opacity = "1";
                    }, ANIMATION_DURATION / 2);
                }, ANIMATION_DURATION / 2);
            }
        };
    }

    function onDomReady() {
        startLoader();
        startAnimations();
    }

    function startAnimations() {
        const loaderProgress = document.querySelector(".opensilver-loader-progress");
        const counterContainer = document.querySelector(".opensilver-counter-container");

        if (loaderProgress) {
            loaderProgress.style.transition = "width 1.25s, opacity 1.25s";
            loaderProgress.style.width = "60vw";
            loaderProgress.style.opacity = "1";
        }

        if (counterContainer) {
            counterContainer.style.transition = "opacity 0.3s";
            counterContainer.style.opacity = "1";
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', onDomReady);
    } else {
        onDomReady();
    }
})();