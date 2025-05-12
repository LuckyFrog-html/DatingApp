"use client"

import React, { useRef } from "react";
import "./style.css";

interface RippleCardProps {
    children: React.ReactNode;
    className?: string;
    style?: React.CSSProperties;
}

export const RippleCard: React.FC<RippleCardProps> = ({
    children,
    className,
    style,
}) => {
    const containerRef = useRef<HTMLDivElement>(null);

    const handleClick = (e: React.MouseEvent<HTMLDivElement>) => {
        const container = containerRef.current;
        if (!container) return;

        const rect = container.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        const ripple = document.createElement("span");
        ripple.className = "ripple";
        ripple.style.left = `${x}px`;
        ripple.style.top = `${y}px`;

        container.appendChild(ripple);

        ripple.addEventListener("animationend", () => {
            ripple.remove();
        });
    };

    return (
        <div
            ref={containerRef}
            onClick={handleClick}
            className={`relative overflow-hidden rounded-xl ${className ?? ""}`}
            style={style}>
            {children}
        </div>
    );
};
