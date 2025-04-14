import { gsap } from "gsap";

interface AnimateValueProps {
    from: number; // Начальное значение
    to: number; // Конечное значение
    duration: number; // Продолжительность анимации в секундах
    onUpdate: (value: number) => void; // Callback для обновления значения
    onComplete?: () => void; // Опциональный callback по завершению
}

export function animateValue({
    from,
    to,
    duration,
    onUpdate,
    onComplete,
}: AnimateValueProps): void {
    const obj = { value: from };

    gsap.to(obj, {
        duration, // продолжительность
        value: to, // конечное значение
        ease: "power2.inOut", // квадратичный easing (аналог easeInOutQuad)
        onUpdate: () => onUpdate(obj.value),
        onComplete,
    });
}
