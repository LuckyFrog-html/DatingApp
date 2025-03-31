import { useEffect, useState } from "react";

export function useWindowCenter(): number[] {
    if (typeof window === "undefined") return [0, 0];

    const [center, setCenter] = useState([
        window.innerWidth / 2,
        window.innerHeight / 2,
    ]);

    useEffect(() => {
        const handleResize = () => {
            setCenter([window.innerWidth / 2, window.innerHeight / 2]);
        };

        window.addEventListener("resize", handleResize);

        return () => window.removeEventListener("resize", handleResize);
    }, []);

    return center;
}
