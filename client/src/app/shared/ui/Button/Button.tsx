import { twMerge } from "tailwind-merge";

export type ButtonProps = {
    children: React.ReactNode;
    className?: string;
    onClick?: () => void;
};

export const Button = ({ children, className, onClick }: ButtonProps) => {
    return (
        <button
            className={twMerge("py-2 px-8 bg-green-200 text-black rounded hover:bg-green-100 hover:cursor-pointer transition-all", className)}
            onClick={onClick}>
            {children}
        </button>
    );
};
