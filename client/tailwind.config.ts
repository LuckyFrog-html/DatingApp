import type { Config } from "tailwindcss";

module.exports = {
    content: ["./src/**/*.{ts,tsx}"],
    important: true,
    theme: {
        extend: {
            spacing: {
                0: "0px",
                px: "1px",
                1: "5px",
                2: "10px",
                3: "15px",
                4: "20px",
                5: "25px",
                6: "30px",
                7: "35px",
                8: "40px",
            },
            colors: {
                green: {
                    50: "C6FDEF",
                    100: "8CF8DD",
                    200: "5FF3CE",
                },
                red: {
                    50: "#FFC7C2",
                    500: "#FF5649",
                },
                orange: {
                    500: "#F39C12",
                },
                gray: {
                    50: "#F6F6F6",
                    100: "#F2F2F2",
                    300: "#C4C4C4",
                    900: "#292929",
                },
                background: "var(--background)",
                foreground: "var(--foreground)",
            },
        },
    },
} satisfies Config;
