import Link from "next/link";
import { Button, ButtonProps } from "../Button";

type Props = ButtonProps & {
    href: string;
};

export const LinkButton = ({ href, ...props }: Props) => {
    return (
        <Link href={href}>
            <Button {...props} />
        </Link>
    );
};
